<?php
    require_once __DIR__.'/BasePage.php';
    require_once __DIR__.'/PageComposer.php';

    class CatalogPage extends BasePage {
        function prepareBody() {
            $body = new PageComposer(__DIR__.'/html/catalog_body.phtml');
            $categoriesHead = new PageComposer(null);
            $currentCategory = $categoriesHead;
            for ($i = 0; $i < 4; ++$i) {
                $filtersHead = new PageComposer(null);
                $currentFilter = $filtersHead;
                for ($j = 0; $j < 4; ++$j) {
                    $currentFilter = $currentFilter
                        ->chain(new PageComposer(__DIR__.'/html/catalog_panel_category_filter.phtml'))
                        ->compose('category', "$i")
                        ->compose('case', "$j")
                        ->compose('name', "Filter ${i}_${j}");
                }
                $currentCategory = $currentCategory
                    ->chain(new PageComposer(__DIR__.'/html/catalog_panel_category.phtml'))
                    ->compose('name', "Panel $i")
                    ->compose('i', $i)
                    ->compose('filters', $filtersHead);
            }
            $body->compose('categoryFragments', $categoriesHead);
            $body->compose('itemCards', '');
            return $body->render();
        }
    }
?>
