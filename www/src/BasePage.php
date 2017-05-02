<?php
    require_once __DIR__.'/PageComposer.php';

    abstract class BasePage {
        function __construct($app) {
            $this->basePage = new PageComposer(__DIR__.'/html/base_page.phtml');
            $this->basePage
                ->compose('search', '');

            $this->session = $app->session;
            $this->db = $app->db;
            // save user info. TODO: refactoring, incapsulate?
            $uid = $this->session['uid'];
            if (isset($uid)) {
                $userStmt = $this->db->query("SELECT * FROM User WHERE id = '$uid'");
                $this->user = $userStmt->fetch();
                $userStmt->closeCursor();
            }
        }

        function prepare() {
            if (isset($this->user)) {
                $authFragment = new PageComposer(__DIR__.'/html/navbar_auth_logged_in.phtml');
                $authFragment->compose('firstName', $this->user['first_name']);
            } else {
                $authFragment = new PageComposer(__DIR__.'/html/navbar_auth_not_logged_in.phtml');
            }
            $this->basePage->compose('authFragment', $authFragment);
            $this->basePage->compose('pageBody', $this->prepareBody());
            return $this;
        }

        function render() {
            echo $this->basePage->render();
        }

        function setSearch($value) {
            $this->basePage->compose('search', $value);
        }

        abstract function prepareBody();

        // template manager
        private $basePage;
        protected $user;
        // dependencies
        protected $session;
        protected $db;
    }
?>
