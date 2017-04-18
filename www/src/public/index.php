<?php

use \Psr\Http\Message\ServerRequestInterface as Request;
use \Psr\Http\Message\ResponseInterface as Response;

require_once '../vendor/autoload.php';
require_once '../CatalogPage.php';

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
    $page = new CatalogPage($this);
    $page->prepare()->render();
    return $response;
});

$app->get('/api/logout', function (Request $request, Response $response) {
	$session = $this->session;
	$session::destroy();
	return $response->withRedirect('/');
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

$app->run();
