<?php

use \Psr\Http\Message\ServerRequestInterface as Request;
use \Psr\Http\Message\ResponseInterface as Response;

require '../vendor/autoload.php';

$app = new \Slim\App;
$app->get('/', function (Request $request, Response $response) {

	ob_start();
		require 'welcome_page.php';
	$page_body = ob_get_clean();

    require 'main.php';
    return $response;
});

$app->get('/login', function (Request $request, Response $response) {
	ob_start();
		require 'login_page.php';
	$page_body = ob_get_clean();

    require 'main.php';
    return $response;
});

$app->post('/api/login', function (Request $request, Response $response) {
	$parsedBody = $request->getParsedBody();
	$body = $response->getBody();
	$body->write("WRONG_PASSWORD");
});

$app->run();
