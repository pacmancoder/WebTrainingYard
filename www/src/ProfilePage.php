<?php
    require_once __DIR__.'/BasePage.php';
    require_once __DIR__.'/PageComposer.php';
    require_once __DIR__.'/Utils.php';
    class ProfilePage extends BasePage {
        function __construct($app, $page) {
            parent::__construct($app);
            if (isset($page)) {
                $this->page = $page;
            } else {
                $this->page = 0;
            }
        }

        function prepareBody() {
            $body =  new PageComposer(__DIR__."/html/profile_body.phtml");
            $maxPages = Utils::getOrdersCount($this->db, $this->user['id']);
            
            $maxPages = ceil($maxPages / $this::ORDERS_ON_PAGE);

            $prevPage = ($this->page == 0) ? 0 : ($this->page - 1);
            $nextPage = ($this->page == $maxPages - 1) ? $maxPages - 1 : ($this->page + 1);

            $orders = Utils::getOrders($this->db, $this->user['id'], $this->page, $this::ORDERS_ON_PAGE);
            $ordersHead = new PageComposer(null);
            $currentOrder = $ordersHead;
            foreach($orders as $order) {
                $status = $order['status'];
                $statusClass = '';
                switch ($order['status']) {
                    case 'PENDING':
                        $status = 'In processing';
                        $statusClass = 'text-info';
                        break;
                    case 'CANCELED':
                        $status = 'Canceled';
                        $statusClass = 'text-danger';
                        break;
                    case 'SHIPPING':
                        $status = 'Shipping';
                        $statusClass = 'text-primary';
                        break;
                    case 'DELIVERED':
                        $status = 'Delivered';
                        $statusClass = 'text-success';
                        break;
                }
                $items = Utils::getOrderItems($this->db, $order['id']);
                $itemsHead = new PageComposer(null);
                $currentItem = $itemsHead;
                foreach($items as $item) {
                    $currentItem = $currentItem
                        ->chain(new PageComposer(__DIR__."/html/profile_order_item.phtml"))
                        ->compose('id', $item['id'])
                        ->compose('name', $item['name'])
                        ->compose('quantity', $item['quantity']);
                }
                $currentOrder = $currentOrder
                    ->chain(new PageComposer(__DIR__."/html/profile_order.phtml"))
                    ->compose('id', $order['id'])
                    ->compose('status', $status)
                    ->compose('statusClass', $statusClass)
                    ->compose('created_at', $order['created_at'])
                    ->compose('updated_at', $order['updated_at'])
                    ->compose('items', $itemsHead);
            }
            return $body
                ->compose('newerState', ($this->page == 0)?'disabled':'')
                ->compose('olderState', ($this->page == $maxPages - 1)?'disabled':'')
                ->compose('nextPage', $nextPage)
                ->compose('prevPage', $prevPage)
                ->compose('fname', $this->user['first_name'])
                ->compose('lname', $this->user['last_name'])
                ->compose('email', $this->user['email'])
                ->compose('phone', $this->user['phone'])
                ->compose('orders', $ordersHead)
                ->render();
        }

        const ORDERS_ON_PAGE = 3.0;

        private $page;
    }
?>