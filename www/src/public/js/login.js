function login_callback(xhr, status) {
  switch(xhr.responseText) {
    case "OK":
      window.location.href = "index.php";
      break;
    case "USER_NOT_EXISTS":
      $("#email_form_group").addClass("has-error has-feedback");
      $("#login_alert").text(
        "User " +  $("#email_field").val() + " not exists!");
      $("#login_alert").removeClass("hidden");
      break;
    case "WRONG_PASSWORD":
      $("#password_form_group").addClass("has-error has-feedback");
      $("#login_alert").text("Wrong password!");
      $("#login_alert").removeClass("hidden");
      break;
    default:
      $("#login_alert").text("Server error!");
      $("#login_alert").removeClass("hidden");
  }
  if (xhr.responseText == "OK") {
    window.location.href = "index.php";
  }
}
function process_login() {
  $("#login_alert").addClass("hidden");
  $("#email_form_group").removeClass("has-error has-feedback");
  $("#password_form_group").removeClass("has-error has-feedback");
  var requestBody = {};
  requestBody["login"] = $("#email_field").val();
  requestBody["password"] = $("#password_field").val();
  $.ajax({
    url: "api/login",
    type: "POST",
    data: requestBody,
    complete: login_callback
  });
}
