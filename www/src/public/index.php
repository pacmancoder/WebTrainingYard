<?php

use \Psr\Http\Message\ServerRequestInterface as Request;
use \Psr\Http\Message\ResponseInterface as Response;

require '../vendor/autoload.php';

$app = new \Slim\App;
$app->get('/', function (Request $request, Response $response) {
    $page_name = 'Добро пожаловать';
    $page_body = 'welcome.php';
    require 'main.php';
    return $response;
});
$app->run();
