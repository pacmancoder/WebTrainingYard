<ul class="nav navbar-nav navbar-right">
  <li>
    <a href="#"
       data-toggle="modal"
       data-target="#login_form">
       <span class="glyphicon glyphicon-log-in"></span>
       Login
     </a>
  </li>
  <li>
    <a href="#"
       data-toggle="modal"
       data-target="#register_form">
       <span class="glyphicon glyphicon-user"></span>
       Register
     </a>
  </li>
</ul>
<!-- Login Modal Form -->
<div id="login_form" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Login</h4>
      </div>
      <div class="modal-body">
        <form>
          <div class="form-group" id="l_email_form_group">
            <label for="l_email_field">E-mail:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="email" class="form-control" id="l_email_field" />
            </div>
          </div>
          <div class="form-group" id="l_pass_form_group">
            <label for="l_pass_field">Password:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" class="form-control" id="l_pass_field" />
            </div>
          </div>
        </form>
        <div class="alert alert-danger hidden" id="l_alert">
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
<script type="text/javascript" src="js/register.js"></script>
<div id="register_form" class="modal fade" role="dialog">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Register</h4>
      </div>
      <div class="modal-body">
        <form>
          <div class="form-group" id="r_email_form_group">
            <label for="r_email_field">E-mail:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-envelope"></i></span>
              <input type="email" class="form-control" id="r_email_field" />
            </div>
          </div>
          <div class="form-group" id="r_pass_form_group">
            <label for="r_pass_field">Password:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" class="form-control" id="r_pass_field" />
            </div>
          </div>
          <div class="form-group" id="r_rpass_form_group">
            <label for="r_rpass_field">Repeat password:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
              <input type="password" class="form-control" id="r_rpass_field" />
            </div>
          </div>
          <div class="form-group" id="r_fname_form_group">
            <label for="r_fname_field">First name:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input class="form-control" id="r_fname_field" />
            </div>
          </div>
          <div class="form-group" id="r_lname_form_group">
            <label for="r_lname_field">Last name:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
              <input class="form-control" id="r_lname_field" />
            </div>
          </div>
          <div class="form-group" id="r_phone_form_group">
            <label for="r_lname_field">Last name:</label>
            <div class="input-group">
              <span class="input-group-addon"><i class="glyphicon glyphicon-earphone"></i></span>
              <input class="form-control" id="r_phone_field" />
            </div>
          </div>
        </form>
        <div class="alert alert-danger hidden" id="r_alert">
          <!-- Content will be set at runtime  -->
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" onclick="process_register()">Register</button>
      </div>
    </div>
  </div>
</div>
