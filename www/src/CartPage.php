<?php
    require_once __DIR__.'/BasePage.php';
    require_once __DIR__.'/PageComposer.php';
    require_once __DIR__.'/Utils.php';

    class CartPage extends BasePage {
        function __construct($app, $delItem) {
            parent::__construct($app);
            if (isset($delItem)) {
                Utils::delCartItem($this->db, $this->user['id'], $delItem);
            }
        }

        function prepareBody() {
            $body =  new PageComposer(__DIR__."/html/cart_body.phtml");
            $warehouses = Utils::getWarehouses($this->db);
            $warehousesHead = new PageComposer(null);
            $currentWarehouse = $warehousesHead;
            foreach($warehouses as $warehouse) {
                $currentWarehouse = $currentWarehouse
                    ->chain(new PageComposer(__DIR__."/html/cart_warehouse_item.phtml"))
                    ->compose('id', $warehouse['id'])
                    ->compose('name', $warehouse['name']);
            }
            $items = Utils::getCartItems($this->db, $this->user['id']);
            $itemsHead = new PageComposer(null);
            $currentItem = $itemsHead;            
            foreach ($items as $item) {
                $iamge = isset($item['image'])?$item['image']:'noimage.jpg';
                $currentItem = $currentItem
                    ->chain(new PageComposer(__DIR__."/html/cart_item.phtml"))
                    ->compose('id', $item['id'])
                    ->compose('name', $item['name'])
                    ->compose('quantity', $item['quantity'])
                    ->compose('cost', $item['price'] * $item['quantity'])
                    ->compose('image', $iamge);
            }
            return $body
                ->compose('warehouses', $warehousesHead)
                ->compose('items', $itemsHead)
                ->render();
        }
    }
?>