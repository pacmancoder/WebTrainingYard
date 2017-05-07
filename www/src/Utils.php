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

        function getOrdersCount($db, $user) {
            $ordersStmt = $db->query("SELECT COUNT(*) AS count FROM `Order` WHERE user_id = $user");
            $orders = $ordersStmt->fetch();
            $ordersStmt->closeCursor();
            return $orders['count'];            
        }

        function getOrders($db, $user, $page, $itemsOnPage) {
            $start = $itemsOnPage * $page;            
            $ordersStmt = $db->query("SELECT id, status, created_at, updated_at FROM `Order` WHERE user_id = $user LIMIT $start, $itemsOnPage");
            $orders = $ordersStmt->fetchAll();
            $ordersStmt->closeCursor();
            return $orders;            
        }

        function getOrderItems($db, $order) {
            $itemsStmt = $db->query("SELECT item_id as id, Item.name AS name, quantity FROM OrderItem JOIN Item ON item_id = Item.id WHERE order_id = $order;");
            $items = $itemsStmt->fetchAll();
            $itemsStmt->closeCursor();
            return $items;                        
        }

    }
?>