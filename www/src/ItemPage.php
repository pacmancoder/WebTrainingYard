<?php
    require_once __DIR__.'/BasePage.php';
    require_once __DIR__.'/PageComposer.php';
    require_once __DIR__.'/BreadcrumbBuilder.php';

    class ItemPage extends BasePage {
        function __construct($app, $id) {
            parent::__construct($app);            
            $this->id = $id;

            $itemStmt = $this->db->query(
                "SELECT Item.name, ".
                    "Item.available, ".
                    "Item.category_id AS category, ".
                    "Item.description AS description, ".
                    "Item.price AS price, ".
                    "Media.url_hd AS image ".
                    "FROM Item ".
                    "LEFT JOIN Media ON Media.item_id = Item.id AND Media.priority = 0 ".
                    "WHERE Item.id = $id");
            $this->itemInfo = $itemStmt->fetch();
            $itemStmt->closeCursor();    

            $mediaStmt = $this->db->query(
                "SELECT Media.url, Media.url_hd FROM Media WHERE Media.item_id = $id ORDER BY Media.priority");
            $this->media = $mediaStmt->fetchAll();
            $mediaStmt->closeCursor();

            $specificationsStmt = $this->db->query(
                "SELECT Specification.name AS name, ".
	                "IFNULL(specificationCase.description, itemSpecification.text) AS value ".
                    "FROM itemSpecification ".
                    "JOIN Specification ON itemSpecification.specification_id = Specification.id ".
                    "LEFT JOIN specificationCase ON itemSpecification.specification_case_id = specificationCase.id ".
                    "WHERE itemSpecification.item_id = $id");
            $this->specifications = $specificationsStmt->fetchAll();
            $specificationsStmt->closeCursor();

            if (isset($this->user)) {
                $user_id = $this->user['id'];

                $this->db->exec("CALL ViewItem($user_id, $id)");
            }
        }

        private function buildCarousel() {
            $carouselHead = new PageComposer(null);
            $currentSlide = $carouselHead;

            $itemsAdded = 4;
            $first = true;
            foreach($this->media as $image) {
                if ($itemsAdded >= 4) {
                    if (!$first) {
                        $currentSlide->compose('items', $itemsHead);
                    }
                    $itemsHead = new PageComposer(null);
                    $currentItem = $itemsHead;         

                    $currentSlide = $currentSlide
                        ->chain(new PageComposer(__DIR__.'/html/item_carousel_slide.phtml'));

                    if ($first) {
                        $currentSlide->compose('active', true);
                    }
                    $itemsAdded = 0;
                    $first = false;
                }
                $currentItem = $currentItem
                    ->chain(new PageComposer(__DIR__.'/html/item_carousel_item.phtml'))
                    ->compose('thumbnailUrl', $image['url'])
                    ->compose('hdUrl', $image['url_hd']);
                $itemsAdded++;
            }
            $currentSlide->compose('items', $itemsHead);    
            return $carouselHead;
        }

        private function buildSpecifications() {
            $specificationsHead = new PageComposer(null);
            $currentSpecification = $specificationsHead;
            foreach($this->specifications as $specification) {
                $currentSpecification = $currentSpecification
                    ->chain(new PageComposer('/html/item_specification_row.phtml'))
                    ->compose('name', $specification['name'])
                    ->compose('value', $specification['value']);
            }

            return $specificationsHead;
        }

        function prepareBody() {
            $body = new PageComposer(__DIR__.'/html/item_body.phtml');
            $breadcrumbBuilder = new BreadcrumbBuilder($this->db, $this->itemInfo['category']);
            $breadcrumb = $breadcrumbBuilder->render();
            return $body
                ->compose('breadcrumb', $breadcrumb)
                ->compose('name', $this->itemInfo['name'])
                ->compose('available', $this->itemInfo['available'])
                ->compose('image', $this->itemInfo['image'])
                ->compose('description', $this->itemInfo['description'])
                ->compose('price', $this->itemInfo['price'])
                ->compose('carouselSlides', $this->buildCarousel())                
                ->compose('specifications', $this->buildSpecifications())
                ->compose('id', $this->id)
                ->render();
        }

        private $id;
        private $itemInfo;
        private $media;
        private $specifications;
    }
?>