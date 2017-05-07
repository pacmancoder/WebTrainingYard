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
            $itemsStmt = $db->query("SELECT item_id as id, Item.name AS name, quantity, OrderItem.price AS price FROM OrderItem JOIN Item ON item_id = Item.id WHERE order_id = $order;");
            $items = $itemsStmt->fetchAll();
            $itemsStmt->closeCursor();
            return $items;                        
        }

        function delCartItem($db, $user, $item) {
            $db->exec("DELETE FROM ShoppingCartItem WHERE user_id = $user AND item_id = $item");
        }

        function getWarehouses($db) {
            $warehousesStmt = $db->query("SELECT id, name FROM Warehouse WHERE is_transport != TRUE");
            $warehouses = $warehousesStmt->fetchAll();
            $warehousesStmt->closeCursor();
            return $warehouses;   
        }

        function getCartItems($db, $user) {
            $itemsStmt = $db->query(
                "SELECT ShoppingCartItem.item_id as id, Item.name AS name, quantity, Item.price AS price, ".
                "Media.url AS image FROM ShoppingCartItem JOIN Item ON ShoppingCartItem.item_id = Item.id ".
                "LEFT JOIN (SELECT * FROM Media WHERE Media.priority = 0) AS Media ON Media.item_id = ShoppingCartItem.item_id ".
                "WHERE user_id = $user ".
                "GROUP BY ShoppingCartItem.item_id "
            );
            $items = $itemsStmt->fetchAll();
            $itemsStmt->closeCursor();
            return $items;  
        }

    }
?>