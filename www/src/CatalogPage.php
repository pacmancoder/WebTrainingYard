<?php
    require_once __DIR__.'/BasePage.php';
    require_once __DIR__.'/PageComposer.php';

    class CatalogPage extends BasePage {
        function __construct($app, $category, $filters, $page) {
            parent::__construct($app);
            $this->category = $category;
            if (isset($page)) {
                $this->page = $page;
            } else {
                $this->page = 0;
            }
            // parse filters
            $this->filters = array();
            if (isset($filters)) {
                $filterStrings = explode(";", $filters);                
                foreach ($filterStrings  as $filterStr) {
                    $filter = explode('=', $filterStr);
                    if (isset($filter[1])) {
                        $this->filters[$filter[0]] = explode(',', $filter[1]);
                    }
                }
            }
        }

        function prepareBody() {
            $body = new PageComposer(__DIR__.'/html/catalog_body.phtml');
            
            // build breadcrumb
            $breadcrumbHead = new PageComposer(null);
            $parentCategory = $this->category;

            do {
                $categoryInfoStmt = $this->db->query(
                    "SELECT name, parent_category FROM Category WHERE id = '$parentCategory'");
                $categoryInfo = $categoryInfoStmt->fetch();
                $categoryInfoStmt->closeCursor();   

                $nextBreadcrumb = new PageComposer(__DIR__.'/html/catalog_breadcrumb_item.phtml');
                $nextBreadcrumb 
                    ->compose('id', $parentCategory)
                    ->compose('name', $categoryInfo['name'])
                    ->chain($breadcrumbHead);
                    
                $breadcrumbHead = $nextBreadcrumb;
                $parentCategory = $categoryInfo['parent_category'];
            } while ($parentCategory != null);

            // build filters            
            $filtersInfoStmt = $this->db->query(
                    "SELECT Specification.id, Specification.name ".
                    "FROM Specification ".
                    "JOIN CategoryHasSpecification ON Specification.id = CategoryHasSpecification.specification_id ".
                    "WHERE CategoryHasSpecification.category_id = '$this->category'"
            );
            $filtersInfo = $filtersInfoStmt->fetchAll();
            $filtersInfoStmt->closeCursor();

            $filtersHead = new PageComposer(null);
            $currentFilter = $filtersHead;

            foreach($filtersInfo as $filter) {                
                // build cases
                $filterId = $filter['id'];
                $casesInfoStmt = $this->db->query(
                        "SELECT SpecificationCase.id, SpecificationCase.description ".
                        "FROM Specification ".
                        "JOIN SpecificationCase ON Specification.id = SpecificationCase.specification_id ".
                        "WHERE Specification.id = $filterId"
                );
                $casesInfo = $casesInfoStmt->fetchAll();
                $casesInfoStmt->closeCursor();

                $casesHead = new PageComposer(null);
                $currentCase = $casesHead;   
                foreach($casesInfo as $case) {
                    $currentCase = $currentCase
                        ->chain(new PageComposer(__DIR__.'/html/catalog_panel_filter_case.phtml'))
                        ->compose('id', $case['id'])
                        ->compose('description', $case['description']);
                    if (array_key_exists($filter['id'], $this->filters)) {
                        if (in_array($case['id'], $this->filters[$filter['id']])) {
                            $currentCase->compose('checked', 'true');
                        }
                    }
                }
                         
                $currentFilter = $currentFilter
                    ->chain(new PageComposer(__DIR__.'/html/catalog_panel_filter.phtml'))
                    ->compose('name', $filter['name'])
                    ->compose('id', $filter['id'])
                    ->compose('cases', $casesHead);
            }

            $filterConditions = '';
            $mergedCases = array();
            foreach ($this->filters as $filterToMerge) {
                $mergedCases = array_merge($mergedCases, $filterToMerge);
            }
            if (!empty($mergedCases)) {
                $filterConditions .= "AND ItemSpecification.specification_case_id IN (" . join(',', $mergedCases) . ") ";
            }

            // Pagination            
            $paginationHead = new PageComposer(null);
            $currentPaginationItem = $paginationHead;

            // Previous button
            $currentPaginationItem = $currentPaginationItem
                ->chain(new PageComposer(__DIR__.'/html/catalog_pagination_item.phtml'))
                ->compose('text', "Previous");
            
            if ($this->page > 0) {
                $currentPaginationItem->compose('state', '');
            } else {
                $currentPaginationItem->compose('state', 'disabled');
            }
            // prepare query for Item count
            $itemsCountQuery = 
                "SELECT COUNT(*) as `count` FROM Item ".
                "JOIN ItemSpecification ON Item.id = ItemSpecification.item_id ".
                "WHERE Item.available > 0 ".
                "AND Item.category_id = " . $this->category . " ".
                $filterConditions.
                "GROUP BY Item.id ";  
            
            // get max page index
            $itemCountStmt = $this->db->query($itemsCountQuery);
            $itemCount = $itemCountStmt->fetch()['count'];
            $maxPageCount = ($itemCount % self::ITEMS_ON_PAGE > 0) ? 1 : 0;
            $maxPageCount += floor($itemCount / self::ITEMS_ON_PAGE);

            // displayed page items (prettyfied)
            $paginationItemsLeft = self::MAX_PAGINATION_COUNT;
            $firstPage = max(0, $this->page - floor(self::MAX_PAGINATION_COUNT / 2));
            $paginationItemsLeft -= $this->page - $firstPage;
            $lastPage = min($maxPageCount - 1, $this->page + $paginationItemsLeft - 1);
            for($i = $firstPage; $i < $lastPage + 1; $i++) {
                $currentPaginationItem = $currentPaginationItem
                    ->chain(new PageComposer(__DIR__.'/html/catalog_pagination_item.phtml'))
                    ->compose('text', $i + 1);
                if ($i == $this->page) {
                    $currentPaginationItem->compose('state', 'active');
                } else {
                    $currentPaginationItem->compose('state', '');
                }
            }

            // Next button
            $currentPaginationItem = $currentPaginationItem
                ->chain(new PageComposer(__DIR__.'/html/catalog_pagination_item.phtml'))
                ->compose('text', "Next");
            
            if ($this->page < $maxPageCount - 1) {
                $currentPaginationItem->compose('state', '');
            } else {
                $currentPaginationItem->compose('state', 'disabled');
            }

            // Item cards
            
            // prepare query
            $itemsQuery = 
                "SELECT Item.id, Item.name, Item.price, Item.description, Media.url AS `mediaLink` ".
                "FROM Item ".
                "JOIN (SELECT Item_id, url FROM Media WHERE priority = 0) Media ON Item.id = Media.Item_id ".
                "JOIN ItemSpecification ON Item.id = ItemSpecification.item_id ".
                "WHERE Item.available > 0 ".
                "AND Item.category_id = " . $this->category . " ".
                $filterConditions;
            // filters for query
            $itemsQuery .= $filterConditions;
            // ignore joined ItemSpecification
            $itemsQuery .= 'GROUP BY Item.id ';       
            // limit for page     
            $pageOffset = self::ITEMS_ON_PAGE * $this->page;
            $pageLimit = self::ITEMS_ON_PAGE;
            $itemsQuery .= "LIMIT $pageOffset, $pageLimit ";            
            
            $itemsStmt = $this->db->query($itemsQuery);
            $items = $itemsStmt->fetchAll();
            $itemsStmt->closeCursor();            

            // build from template
            $itemCardsHead = new PageComposer(null);
            $currentItemCard = $itemCardsHead;
            foreach ($items as $currentItem) {                
                $currentItemCard = $currentItemCard
                    ->chain(new PageComposer(__DIR__.'/html/catalog_item_card.phtml'))
                    ->compose('id', $currentItem['id'])
                    ->compose('name', $currentItem['name'])
                    ->compose('description', $currentItem['description'])
                    ->compose('price', $currentItem['price'] . ' $')
                    ->compose('mediaLink', '/img/'.$currentItem['mediaLink']);
            }

            $body->compose('filters', $filtersHead)
                 ->compose('itemCards', $itemCardsHead)
                 ->compose('paginationItems', $paginationHead)
                 ->compose('breadcrumbItems', $breadcrumbHead)
                 ->compose('category', $this->category)
                 ->compose('page', $this->page);

            return $body->render();

        }

        // constants
        const ITEMS_ON_PAGE = 9;  
        const MAX_PAGINATION_COUNT = 5;  

        // Private fields
        private $category;
        private $filters;
        private $page;
    }
?>
