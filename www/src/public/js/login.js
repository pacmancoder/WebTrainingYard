function login_callback(xhr, status) {
  switch(xhr.responseText) {
    case "OK":
      window.location.href = "index.php";
      break;
    case "USER_NOT_EXISTS":
      $("#l_email_form_group").addClass("has-error has-feedback");
      $("#l_alert").text(
        "User " +  $("#l_email_field").val() + " not exists!");
      $("#l_alert").removeClass("hidden");
      break;
    case "WRONG_PASSWORD":
      $("#l_pass_form_group").addClass("has-error has-feedback");
      $("#l_alert").text("Wrong password!");
      $("#l_alert").removeClass("hidden");
      break;
    default:
      $("#l_alert").text("Server error!");
      $("#l_alert").removeClass("hidden");
  }
}

function process_login() {
  $("#l_alert").addClass("hidden");
  $("#l_email_form_group").removeClass("has-error has-feedback");
  $("#l_pass_form_group").removeClass("has-error has-feedback");
  var requestBody = {};
  requestBody["email"] = $("#l_email_field").val();
  requestBody["password"] = $("#l_pass_field").val();
  $.ajax({
    url: "api/login",
    type: "POST",
    data: requestBody,
    complete: login_callback
  });
}
