<?php
    require_once __DIR__.'/PageComposer.php';

    class BreadcrumbBuilder {
        function __construct($db, $category) {
            $this->db = $db;
            $this->category = $category;
        }
        function render() {
            $breadcrumb = new PageComposer('/html/breadcrumb.phtml');

            $breadcrumbItemsHead = new PageComposer(null);
            $parentCategory = $this->category;

            do {
                $categoryInfoStmt = $this->db->query(
                    "SELECT name, parent_category FROM Category WHERE id = '$parentCategory'");
                $categoryInfo = $categoryInfoStmt->fetch();
                $categoryInfoStmt->closeCursor();   
                if ($categoryInfo['parent_category'] == null) {
                    break;
                }

                $nextBreadcrumbItem = new PageComposer(__DIR__.'/html/breadcrumb_item.phtml');
                $nextBreadcrumbItem 
                    ->compose('id', $parentCategory)
                    ->compose('name', $categoryInfo['name'])
                    ->chain($breadcrumbItemsHead);
                    
                $breadcrumbItemsHead = $nextBreadcrumbItem;
                $parentCategory = $categoryInfo['parent_category'];
            } while (true);
            $breadcrumb->compose('breadcrumbItems', $breadcrumbItemsHead);
            return $breadcrumb;
        }
        private $db;
        private $category;
    }
?>