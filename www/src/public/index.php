<?php

use \Psr\Http\Message\ServerRequestInterface as Request;
use \Psr\Http\Message\ResponseInterface as Response;

require_once __DIR__.'/../vendor/autoload.php';
require_once __DIR__.'/../CatalogPage.php';
require_once __DIR__.'/../ItemPage.php';
require_once __DIR__.'/../ProfilePage.php';
require_once __DIR__.'/../CartPage.php';

$app = new \Slim\App;

$app->add(new \Slim\Middleware\Session([
  'name' => 'main_session',
  'autorefresh' => true,
  'lifetime' => '1 hour'
]));

$container = $app->getContainer();

$container['session'] = function ($c) {
  return new \SlimSession\Helper;
};

$container['db'] = function ($c) {
    $pdo = new PDO('mysql:host=localhost;dbname=pcbuilder_db;charset=utf8', 'root', '');
    $pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
    $pdo->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_ASSOC);
    return $pdo;
};

$app->get('/', function (Request $request, Response $response) {
    return $response->withRedirect('/catalog');
});

$app->get('/catalog[/{category}[/{page}[/{filter}]]]', function (Request $request, Response $response) {
    $page = new CatalogPage(
        $this, 
        $request->getAttribute('category'), 
        $request->getAttribute('filter'),
        $request->getAttribute('page'));
    $page->prepare()->render();
    return $response;
});

$app->get('/item/{id}', function (Request $request, Response $response) {
    $page = new ItemPage(
        $this,
        $request->getAttribute('id'));
    $page->prepare()->render();
    return $response;
});

$app->get('/profile[/orders/{page}]', function (Request $request, Response $response) {
    $page = new ProfilePage(
        $this,
        $request->getAttribute('page'));
    $page->prepare()->render();
    return $response;    
});

$app->get('/cart[/remove/{delItem}]', function (Request $request, Response $response) {
    $page = new CartPage(
        $this,
        $request->getAttribute('delItem'));
    $page->prepare()->render();
    return $response;    
});

$app->get('/api/logout', function (Request $request, Response $response) {
	$session = $this->session;
	$session::destroy();
	return $response->withRedirect('/');
});

$app->get('/api/orderItems/{warehouse}', function (Request $request, Response $response) {
	$session = $this->session;
    $db = $this->db;
    $uid = $session->uid;
    $warehouse =  $request->getAttribute('warehouse');
	$db->exec("CALL OrderCart($uid, $warehouse)");
	return $response->withRedirect('/profile');
});

$app->post('/api/login', function (Request $request, Response $response) {
    $session = $this->session;
    $db = $this->db;

    $parsedBody = $request->getParsedBody();
    $email = $parsedBody['email'];
    $password = $parsedBody['password'];

    $stmt = $db->query("SELECT id, email, password_hash FROM User WHERE email = '$email'");
    $dbUser = $stmt->fetch();

    $responseBody = $response->getBody();

    if (!$dbUser) {
        $responseBody->write("USER_NOT_EXISTS");
    } else if (!password_verify($password, $dbUser['password_hash'])){
        $responseBody->write("WRONG_PASSWORD");
    } else {
        $session->uid = $dbUser['id'];
        $responseBody->write("OK");
    }
    return $response;
});

$app->get('/api/reserve/{id}', function (Request $request, Response $response) {
    $session = $this->session;
    $db = $this->db;
    $responseBody = $response->getBody();    
    if (!isset($session->uid)) {
        $responseBody->write("NOT LOGGED IN");
    } else {
        $user_id = $session->uid;
        $item = $request->getAttribute('id');
        try {
            $db->exec("CALL AddToCart($user_id, $item, 1)");
            $responseBody->write("OK");            
        } catch (PDOException $e) {
            switch ($e->getCode()) {
                case 45000:
                    $responseBody->write("NOT AVAILABLE");
                    break;
                case 23000:
                    $responseBody->write("ALREADY IN CART");
                    break;                    
                default:
                    $responseBody->write("SERVER ERROR");
            }            
        }
    }
    return $response;
});

$app->post('/api/register', function (Request $request, Response $response) {
    // destroy previous session
    $session = $this->session;
	$session->clear();

    $responseBody = $response->getBody();

    $user = $request->getParsedBody();
    $email = $user['email'];
    // check existance of user
    $db = $this->db;
    $existingUsersStmt = $db->query("SELECT email, password_hash FROM User WHERE email = '$email'");
    $existingUsers = $existingUsersStmt->fetch();
    $existingUsersStmt->closeCursor();
    if ($existingUsers != false) {
        $responseBody->write("USER_EXISTS");
    } else {
        $db->exec('INSERT INTO User(email, password_hash, phone, first_name, last_name) VALUES(' .
            "'" . $user['email'] . "', " .
            "'" . password_hash($user['password'], PASSWORD_DEFAULT) . "', " .
            "'" . $user['phone'] . "', " .
            "'" . $user['fname'] . "', " .
            "'" . $user['lname'] . "')"
        );

        $dbUserStmt = $db->query("SELECT id FROM User WHERE email = '$email'");
        $dbUser = $dbUserStmt->fetch();
        $dbUserStmt->closeCursor();

        $session->uid = $dbUser['id'];
        $responseBody->write("OK");
    }
    return $response;
});

$app->post('/api/chpass', function (Request $request, Response $response) {
	$session = $this->session;
    $db = $this->db;
    $newPass = password_hash($request->getParsedBody()['pass'], PASSWORD_DEFAULT);    
    $id = $session->uid;
    $db->exec("UPDATE User SET password_hash = \"$newPass\" WHERE id = $id");
    $responseBody = $response->getBody(); 
    $responseBody->write("OK");       
});

$app->run();