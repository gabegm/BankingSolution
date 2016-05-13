/// <reference path="../jquery-2.2.1.js" />

$(document).ready(function () {
    Initialise();

    $('#btnSubmit').click(function (e) {
        Validate(e);
    });
});

function Clear() {
    $("input[type=\"checkbox\"]").prop("checked", false);
}

function Initialise() {
    $(".inputField").width(110);
    $(".inputField").css('background-color', 'green');
}

function Validate(e) {
    var message = "";
    var usernameCheck = true;
    var passwordCheck = true;

    if ($('#txtUsername').val().length == 0) {
        usernameCheck = false;
        message += "Username cannot be lect blank";
    }

    if ($('#txtPassword').val().length == 0) {
        passwordCheck = false;
        message += "Passowrd cannot be lect blank";
    }

    $('#lblMessage').text(message);

    if (usernameCheck == false || passwordCheck == false) {
        e.preventDefault();
    }
}

function Show() {
    $("#divMain").show();
}

function Hide() {
    $("#divMain").hide();
}