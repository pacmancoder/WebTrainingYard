class Validator {
    static isEmail(value) {
        var re = /\S+@\S+\.\S+/;
        return re.test(value);
    }

    static isPhone(value)
    {
        var re = /\+?\d{10,}/;
        return re.test(value);
    }

    static isPassword(value) {
        return (value.length >= 6);
    }
}

class InputFormManager {
    constructor(alertControl, controlMap) {
        this.alert = alertControl;
        this.controls = controlMap;
    }

    clearErrors() {
        $(this.alert).addClass("hidden");
        $(this.alert).empty()
        for (var key in this.controls) {
          $(this.controls[key]).removeClass("has-error has-feedback");
        }
    }

    setError(field, message) {
        if (message != null) {
            $(this.alert).append("<p>" + message + "</p>");
            $(this.alert).removeClass("hidden");
        }
        var control = this.controls[field];
        if (control != undefined) {
            $(control).addClass("has-error has-feedback");
        }
    }

    getValue(field) {
        var control = this.controls[field];
        if (control != undefined) {
            return $(control + " .form-control").val();
        } else {
            return null;
        }
    }
}

function process_login() {
    // prepare GUI hook
    var form = new InputFormManager(
        "#l_alert",
        {
        "email" :  "#l_email_form_group",
        "password" : "#l_pass_form_group"
        }
    );
    form.clearErrors();
    // validation before request
    if (!Validator.isEmail(form.getValue("email"))) {
        form.setError("email", "Invalid email");
        return;
    }
    if (!Validator.isPassword(form.getValue("password"))) {
        form.setError("password", "Too short password");
        return;
    }
    // Build AJAX request
    var requestBody = {};
    requestBody["email"] = $("#l_email_field").val();
    requestBody["password"] = $("#l_pass_field").val();
    $.ajax({
        url: "/api/login",
        type: "POST",
        data: requestBody,
        complete: function(xhr, status) {
            switch(xhr.responseText) {
                case "OK":
                    window.location.href = "/index.php";
                    break;
                case "USER_NOT_EXISTS":
                    form.setError("email",
                        "User " +  $("#l_email_field").val() + " not exists!");
                    break;
                case "WRONG_PASSWORD":
                    form.setError("password", "Wrong password!")
                    break;
                default:
                    form.setError(null, "Server error!")
            }
        }
    });
}

function process_register() {
    // prepare GUI hook
    var form = new InputFormManager(
        "#r_alert",
        {
            "email" :  "#r_email_form_group",
            "password" : "#r_pass_form_group",
            "rpassword" : "#r_rpass_form_group",
            "fname" : "#r_fname_form_group",
            "lname" : "#r_lname_form_group",
            "phone" : "#r_phone_form_group"
        }
    );
    form.clearErrors();
    // Validate input
    if (!Validator.isEmail(form.getValue("email"))) {
        form.setError("email", "Invalid email!");
        return;
    }
    if (!Validator.isPassword(form.getValue("password"))) {
        form.setError("password", "Too short password!");
        return;
    }
    if (form.getValue("password") != form.getValue("rpassword")) {
        form.setError("password", null);
        form.setError("rpassword", "Passwords are different!");
        return;
    }
    if (form.getValue("fname") == "") {
        form.setError("fname", "First name filed is empty!");
        return;
    }
    if (form.getValue("lname") == "") {
        form.setError("lname", "Last name filed is empty!");
        return;
    }
    if (!Validator.isPhone(form.getValue("phone"))) {
        form.setError("phone", "Invalid phone format!");
    }
    // Build AJAX request
    var requestBody = {};
    requestBody["email"] = form.getValue("email");
    requestBody["password"] = form.getValue("password");
    requestBody["fname"] = form.getValue("fname");
    requestBody["lname"] = form.getValue("lname");
    requestBody["phone"] = form.getValue("phone");
    $.ajax({
        url: "/api/register",
        type: "POST",
        data: requestBody,
        complete: function(xhr, status) {
            switch(xhr.responseText) {
                case "OK":
                  window.location.href = "/index.php";
                  break;
                case "USER_EXISTS":
                  form.setError("email", "User already exists!");
                  break;
                default:
                  form.setError(null, "Server error!");
            }
        }
    });
}

function process_catalog() {
    var filters = {};

    $(".__filter").each(function() {
        var caseList = [];
        $(this).find('.__case:checked').each(function() {
            caseList.push($(this).data('id'));
        });
        if (caseList.length > 0) {
            filters[$(this).data('id')] = caseList.join();
        }   
    });
    var categoryStr = $('.__catalog').data('category');
    var pageStr = $('.__catalog').data('page');
    var filterStr = '';
    if (Object.keys(filters).length > 0) {
        filterStr += '/';
        $.each(filters, function(index, value) {
            if (index != 0) {
                filterStr += ';';
            }
            filterStr += index + '=' + value;
        });
        var categoryStr = $('.__catalog').data('category');
        var pageStr = $('.__catalog').data('page');
    }
    var searchStr = '';
    if ($('.__catalog').data('search')) {
        if (!filterStr) {
            searchStr += '/';
        }
        searchStr += '!' + $('.__catalog').data('search');
    }
    window.location.href = '/catalog/' + categoryStr + '/' + pageStr + filterStr + searchStr;
}

function process_search() {
    $('.__catalog').data('search', $('.__search').val());
    process_catalog();
    event.preventDefault();
}

function process_change_password() {
    if (!$("#p_pass_field").val()) {
        return;
    }
    if (!Validator.isPassword($("#p_pass_field").val())) {
        $("#p_alert").removeClass("hidden");
        $("#p_alert").addClass("alert-danger");
        $("#p_alert").text("Too short password");
        return;
    }
    if ($("#p_pass_field").val() != $("#p_rpass_field").val()) {
        $("#p_alert").removeClass("hidden");
        $("#p_alert").addClass("alert-danger");
        $("#p_alert").removeClass("alert-success");        
        $("#p_alert").text("Passwords are different");
        return;
    }
    // Build AJAX request
    var requestBody = {};
    requestBody["pass"] = $("#p_pass_field").val();
    $.ajax({
        url: "/api/chpass",
        type: "POST",
        data: requestBody,
        complete: function(xhr, status) {
            switch(xhr.responseText) {
                case "OK":
                    $("#p_alert").removeClass("hidden");
                    $("#p_alert").addClass("alert-success");
                    $("#p_alert").removeClass("alert-danger");           
                    $("#p_alert").text("Password was changed");
                    break;
                default:
                    $("#p_alert").removeClass("hidden");
                    $("#p_alert").addClass("alert-danger");
                    $("#p_alert").removeClass("alert-success");        
                    $("#p_alert").text("Server error");
            }
        }
    });    
}

function process_reserve_item(item) {
    $.ajax({
        url: "/api/reserve/" + item,
        type: "GET",
        complete: function(xhr, status) {
            switch(xhr.responseText) {
                case "OK":
                    $("#__cart_alert").removeClass("hidden");
                    $("#__cart_alert").removeClass("alert-danger");
                    $("#__cart_alert").addClass("alert-success");
                    $("#__cart_alert").text("Item was added to your cart!");
                    break;
                case "NOT AVAILABLE":
                    $("#__cart_alert").removeClass("hidden");                
                    $("#__cart_alert").removeClass("alert-success");
                    $("#__cart_alert").addClass("alert-danger");
                    $("#__cart_alert").text("All available items were reserved, try again later!");
                    break;                
                case "ALREADY IN CART": 
                    $("#__cart_alert").removeClass("hidden");                
                    $("#__cart_alert").removeClass("alert-success");
                    $("#__cart_alert").addClass("alert-danger");
                    $("#__cart_alert").text("Item already in shopping cart!");
                    break;                                
                case "NOT LOGGED IN":
                    $("#__cart_alert").removeClass("hidden");                
                    $("#__cart_alert").removeClass("alert-success");
                    $("#__cart_alert").addClass("alert-danger");
                    $("#__cart_alert").text("Please sign in before ordering items!");
                    break;
                default:
                    $("#__cart_alert").removeClass("hidden");                
                    $("#__cart_alert").removeClass("alert-success");
                    $("#__cart_alert").addClass("alert-danger");
                    $("#__cart_alert").text("Server error");
            }
        }
    });  
}

$(document).ready(function(){
    $(".__pagination-item").click(function() {
        if ($(this).hasClass('disabled')) {
            return;
        }
        if ($(this).text().trim() == 'Previous') {
            var currentStr = parseInt($('.__catalog').data('page'));
            $('.__catalog').data('page', --currentStr);
        } else if ($(this).text().trim() == 'Next') {
            var currentStr = parseInt($('.__catalog').data('page'));
            $('.__catalog').data('page', ++currentStr);
        } else {
            var currentStr = parseInt($('.__catalog').data('page'));
            $('.__catalog').data('page', parseInt($(this).text().trim()) - 1);
        }
        process_catalog();
    });
    $(".__carousel-item>img").click(function() {
        $("#__item-image").attr('src', $(this).data('hd-url')); 
    });  
    $('#myCarousel').carousel({
	    interval: 10000
	});
});