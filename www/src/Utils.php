<?php
    class Utils {

        function getCategoriesIdRecursive($db, $parentCategory) {
            $out[] = $parentCategory;
            $categoryStmt = $db->query("SELECT id FROM Category WHERE parent_category = $parentCategory");    
            $childs = $categoryStmt->fetchAll();
            $categoryStmt->closeCursor();
            foreach($childs as $i) {
                $out = array_merge($out, Utils::getCategoriesIdRecursive($db, $i['id']));
            }
            return $out;
        }

        function getChildCategoriesInfo($db, $parentCategory) {
            $categoryStmt = $db->query("SELECT id, name FROM Category WHERE parent_category = $parentCategory");
            $childs = $categoryStmt->fetchAll();
            $categoryStmt->closeCursor();
            return $childs;
        }

    }
?>