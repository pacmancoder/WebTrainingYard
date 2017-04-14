function set_register_error(field, message) {
  $("#r_alert").removeClass("hidden");
  $("#r_alert").text(message);
  switch (field) {
    case "email":
      $("#r_email_form_group").addClass("has-error has-feedback");
      break;
    case "password":
      $("#r_pass_form_group").addClass("has-error has-feedback");
      $("#r_rpass_form_group").addClass("has-error has-feedback");
      break;
    case "phone":
      $("#r_phone_form_group").addClass("has-error has-feedback");
      break;
    case "fname":
      $("#r_fname_form_group").addClass("has-error has-feedback");
      break;
    case "lname":
      $("#r_lname_form_group").addClass("has-error has-feedback");
      break;
  }
}

function register_callback(xhr, status) {
  switch(xhr.responseText) {
    case "OK":
      window.location.href = "index.php";
      break;
    case "USER_EXISTS":
      set_register_error(null, "User already exists!");
      break;
  }
}

function validateEmail(email)
{
    var re = /\S+@\S+\.\S+/;
    return re.test(email);
}

function validatePhone(phone)
{
    var re = /\d+/;
    return re.test(phone);
}

function process_register() {
  $("#r_alert").addClass("hidden");
  var groups = ["#r_email_form_group", "#r_pass_form_group",
                "#r_rpass_form_group", "#r_fname_form_group",
                "#r_lname_form_group", "#r_phone_form_group"];
  for (var i = 0; i < groups.length; ++i) {
    $(groups[i]).removeClass("has-error has-feedback");
  }
  var requestBody = {};
  // make error detector function
  requestBody["email"] = $("#r_email_field").val();
  if (!validateEmail(requestBody["email"])) {
    set_register_error("email", "Invalid e-mail!");
    return;
  }
  requestBody["password"] = $("#r_pass_field").val();
  if (requestBody["password"] != $("#r_rpass_field").val()) {
    set_register_error("password", "Passwords are different!");
    return;
  } else if (requestBody["password"].length < 6) {
    set_register_error("password", "Too short password!");
    return;
  }
  requestBody["fname"] = $("#r_fname_field").val();
  if (requestBody["fname"] == "") {
    set_register_error("fname", "Empty first name!");
    return;
  }
  requestBody["lname"] = $("#r_lname_field").val();
  if (requestBody["lname"] == "") {
    set_register_error("lname", "Empty last name!");
    return;
  }
  requestBody["phone"] = $("#r_phone_field").val();
  if (!validatePhone(requestBody["phone"])) {
    set_register_error("phone", "Invalid phone format!");
    return;
  }

  $.ajax({
    url: "api/register",
    type: "POST",
    data: requestBody,
    complete: register_callback
  });
}
