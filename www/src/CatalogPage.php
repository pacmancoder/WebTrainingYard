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
            // get category name
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

            $filtersHead = new PageComposer(null);
            $currentFilter = $filtersHead;
            for ($i = 0; $i < 4; ++$i) {
                $casesHead = new PageComposer(null);
                $currentCase = $casesHead;
                for ($j = 0; $j < 4; ++$j) {
                    $currentCase = $currentCase
                        ->chain(new PageComposer(__DIR__.'/html/catalog_panel_filter_case.phtml'))
                        ->compose('id', $j)
                        ->compose('filter', $i)
                        ->compose('description', "Filter ${i}_${j}");
                    if (array_key_exists($i, $this->filters)) {
                        if (in_array($j, $this->filters[$i])) {
                            $currentCase->compose('checked', 'true');
                        }
                    }
                }
                $currentFilter = $currentFilter
                    ->chain(new PageComposer(__DIR__.'/html/catalog_panel_filter.phtml'))
                    ->compose('name', "Panel $i")
                    ->compose('id', $i)
                    ->compose('cases', $casesHead);
            }
            $paginationHead = new PageComposer(null);
            $currentPaginationItem = $paginationHead;
            $currentPaginationItem = $currentPaginationItem
                ->chain(new PageComposer(__DIR__.'/html/catalog_pagination_item.phtml'))
                ->compose('text', "Previous")
                ->compose('state', 'disabled');
            for ($i = 0; $i < 4; ++$i) {
                $currentPaginationItem = $currentPaginationItem
                    ->chain(new PageComposer(__DIR__.'/html/catalog_pagination_item.phtml'))
                    ->compose('text', $i + 1);
                if ($i == 0) {
                    $currentPaginationItem->compose('state', 'active');
                } else {
                    $currentPaginationItem->compose('state', '');
                }
            }
            $currentPaginationItem = $currentPaginationItem
                ->chain(new PageComposer(__DIR__.'/html/catalog_pagination_item.phtml'))
                ->compose('text', "Next")
                ->compose('state', '');

            $body->compose('filters', $filtersHead)
                 ->compose('itemCards', '')
                 ->compose('paginationItems', $paginationHead)
                 ->compose('breadcrumbItems', $breadcrumbHead)
                 ->compose('category', $this->category)
                 ->compose('page', $this->page);
            return $body->render();

        }

        // Private fields
        private $category;
        private $filters;
        private $page;
    }
?>
