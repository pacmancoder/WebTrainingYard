<ul class="nav navbar-nav navbar-right">
  <li>
    <a href="#"
       data-toggle="modal"
       data-target="#loginForm">
       <span class="glyphicon glyphicon-log-in"></span>
       Login
     </a>
  </li>
  <li>
    <a href="#"
       data-toggle="modal"
       data-target="#registerForm">
       <span class="glyphicon glyphicon-user"></span>
       Register
     </a>
  </li>
</ul>
<!-- Login Modal Form -->
<script type="text/javascript" src="js/login.js"></script>
<div id="loginForm" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Login</h4>
      </div>
      <div class="modal-body">
        <form>
          <div class="form-group" id="email_form_group">
            <label for="email_field">E-mail:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input type="email" class="form-control" id="email_field" />
            </div>
          </div>
          <div class="form-group" id="password_form_group">
            <label for="email_field">Password</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" class="form-control" id="password_field" />
            </div>
          </div>
        </form>
        <div class="alert alert-danger hidden" id="login_alert">
          <!-- Content will be set at runtime  -->
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" onclick="process_login()">Login</button>
      </div>
    </div>
  </div>
</div>
<!-- Register Modal Form -->
<!--  TODO -->
