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
            return $body
                ->compose('fname', $this->user['first_name'])
                ->compose('lname', $this->user['last_name'])
                ->compose('email', $this->user['email'])
                ->compose('phone', $this->user['phone'])
                ->render();
        }

        private $page;
    }
?>