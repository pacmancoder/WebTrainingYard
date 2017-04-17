<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <title>title</title>
    <!-- CSS -->
    <link href="bower_resources/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Bootstrap core JavaScript -->
    <script src="bower_resources/jquery/dist/jquery.min.js"></script>
    <script src="bower_resources/bootstrap/dist/js/bootstrap.min.js"></script>
    <!--  User-defined script -->
    <script type="text/javascript" src="js/main.js"></script>
  </head>
  <body>
    <div class="container">
    <!-- Navbar -->
    <nav class="navbar" role="navigation">
      <div class="container-fluid">
        <div class="navbar-header">
          <a class="navbar-brand" href="#">PCBuilder</a>
        </div>
        <ul class="nav navbar-nav">
            <li class="active"><a href="#">Link</a></li>
            <li><a href="#">Link</a></li>
            <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Dropdown <b class="caret"></b></a>
                <ul class="dropdown-menu">
                    <li><a href="#">Action</a></li>
                    <li><a href="#">Another action</a></li>
                    <li><a href="#">Something else here</a></li>
                    <li class="divider"></li>
                    <li><a href="#">Separated link</a></li>
                    <li class="divider"></li>
                </ul>
            </li>
        </ul>
    		<?php
    			if (isset($user)) {
    				require 'fragments/navbar_auth_logged_in.php';
    			} else {
                    require 'fragments/navbar_auth_not_logged_in.php';
    			}
    		?>
        <form class="navbar-form" role="search">
            <div class="input-group col-xs-4">
                <input type="text" class="form-control" placeholder="Search" name="q">
                <div class="input-group-btn">
                    <button class="btn btn-default" type="submit">
                      <i class="glyphicon glyphicon-search"></i>
                    </button>
                </div>
            </div>
        </form>
      </div>
    </nav>

  	<?php echo $page_body ?>

    </div>
  </body>
</html>
