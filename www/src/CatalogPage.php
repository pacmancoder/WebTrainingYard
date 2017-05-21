<?php
    require_once __DIR__.'/BasePage.php';
    require_once __DIR__.'/PageComposer.php';
    require_once __DIR__.'/Utils.php';
    require_once __DIR__.'/BreadcrumbBuilder.php';

    class CatalogPage extends BasePage {
        function __construct($app, $category, $filters, $page) {
            parent::__construct($app);
            if (isset($category)) {
                $this->category = $category;
            } else {
                $this->category = 1;
            }
            if (isset($page)) {
                $this->page = $page;
            } else {
                $this->page = 0;
            }
            // parse filters
            $this->filters = array();
            if (isset($filters)) {
                $filterSearchStrings = explode("!", $filters);
                $filterStrings = explode(";", $filterSearchStrings[0]);                
                foreach ($filterStrings  as $filterStr) {
                    $filter = explode('=', $filterStr);
                    if (isset($filter[1])) {
                        $this->filters[$filter[0]] = explode(',', $filter[1]);
                    }
                }
                if (isset($filterSearchStrings[1])) {
                    $this->search = $filterSearchStrings[1];
                    $this->setSearch($this->search);
                } 
            }
        }

        function prepareBody() {
            $body = new PageComposer(__DIR__.'/html/catalog_body.phtml');
            
            // build breadcrumb
            $breadcrumbBuilder = new BreadcrumbBuilder($this->db, $this->category);
            $breadcrumb = $breadcrumbBuilder->render();

            // category + childs recursive
            $recursiveCategory = join(', ', Utils::getCategoriesIdRecursive($this->db, $this->category));

            // build filters            
            $filtersInfoStmt = $this->db->query(
                    "SELECT Specification.id, Specification.name ".
                    "FROM Specification ".
                    "JOIN CategoryHasSpecification ON Specification.id = CategoryHasSpecification.specification_id ".
                    "WHERE CategoryHasSpecification.category_id IN (" . $this->category. ") "
            );
            $filtersInfo = $filtersInfoStmt->fetchAll();
            $filtersInfoStmt->closeCursor();

            // show panels
            $panels = '';
            if (count($filtersInfo) > 0) {
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
                $panels = $filtersHead;
            } else { // show categories panel
                $categoriesInfo = Utils::getChildCategoriesInfo($this->db, $this->category);
                if (count($categoriesInfo) > 0) {
                    $categoriesPanel = new PageComposer(__DIR__.'/html/catalog_panel_categories.phtml');
                    $categoriesHead = new PageComposer(null);
                    $currentCategory = $categoriesHead;
                    foreach($categoriesInfo as $categoryInfo) {
                        $currentCategory = $currentCategory
                            ->chain(new PageComposer(__DIR__.'/html/catalog_panel_categories_item.phtml'))
                            ->compose('id', $categoryInfo['id'])
                            ->compose('name', $categoryInfo['name']);
                    }
                    $categoriesPanel->compose('items', $categoriesHead);
                    $panels = $categoriesPanel;
                }
            }

            // build filter conditions for queries
            $filterConditions = '';
            if (isset($this->search)) {
                $filterConditions .= 
                    "AND (Item.name LIKE '%" . $this->search . 
                        "%' OR Item.description LIKE '%" . $this->search . "%') ";
            }
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
                "LEFT JOIN ItemSpecification ON Item.id = ItemSpecification.item_id ".
                "WHERE Item.available > 0 ".
                "AND Item.category_id IN (" . $recursiveCategory  . ") ".
                $filterConditions.
                "GROUP BY Item.id HAVING COUNT(Item.id) >= ". count($this->filters) . ' ';  
            
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
                "LEFT JOIN (SELECT Item_id, url FROM Media WHERE priority = 0) Media ON Item.id = Media.Item_id ".
                "LEFT JOIN ItemSpecification ON Item.id = ItemSpecification.item_id ".
                "WHERE Item.available > 0 ".
                "AND Item.category_id IN (" . $recursiveCategory  . ") ".
                $filterConditions;
            // filters for query
            $itemsQuery .= $filterConditions;
            // ignore joined ItemSpecification
            $itemsQuery .= 'GROUP BY Item.id HAVING COUNT(Item.id) >= '. count($this->filters) . ' ';       
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
                    ->compose('price', $currentItem['price'])
                    ->compose('mediaLink',  isset($currentItem['mediaLink'])?'/img/'.$currentItem['mediaLink']:'/img/noimage.jpg');
            }

            $searchFragment = isset($this->search) ? $this->search : '';

            $body->compose('panels', $panels)
                 ->compose('itemCards', $itemCardsHead)
                 ->compose('paginationItems', $paginationHead)
                 ->compose('breadcrumb', $breadcrumb)
                 ->compose('category', $this->category)
                 ->compose('page', $this->page)           
                 ->compose('search', $searchFragment);

            return $body->render();
        }

        // constants
        const ITEMS_ON_PAGE = 9;  
        const MAX_PAGINATION_COUNT = 5;  

        // Private fields
        private $category;
        private $filters;
        private $search;
        private $page;
    }
?>
