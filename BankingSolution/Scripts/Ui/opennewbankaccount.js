$(document).ready(function () {
    getAccountTypes();
    getCurrencies();
    getBankAccounts();
});

function getAccountTypes() {

    var url = 'http://localhost:51901/api/BankAccountsApi/GetAccountTypes'; /* retrieves a list of users in json format*/

    $.getJSON(url, function (data) {
        var table = "<select onchange=\"getDuration()\" id=\"ddlAccountTypes\" class=\"form-control\">";
        table += "<option value=\"-1\">Select Account Type</option>";

        $.each(data, function (key, val) {
            table += "<option value=\"" + val.Id + "\">" + val.Name + "</option>";
        });
        table += "</select>";

        $('#divAccountTypes').html(table);
    });
}

function getCurrencies() {

    var url = "http://api.fixer.io/latest";

    $.getJSON(url, function (data) {
        var table = "<select id=\"ddlCurrencies\" class=\"form-control\">";
        table += "<option value=\"-1\">Select Currency</option>";

        table += "<option value=\"EUR\">EUR</option>";

        $.each(data.rates, function (key, val) {
            table += "<option value=\"" + key + "\">" + key + "</option>";
        });

        table += "</select>";
        $('#divCurrencies').html(table);
    });
}

function OpenAccount() {
    var currency = $("#ddlCurrencies").val();
    var type = $("#ddlAccountTypes").val();
    var balance = $("#txtBalance").val();
    var username = $("#hiddenUsername").val();
    var from = $("#ddlAccounts").val();
    var duration = $("#ddlDuration").val();

    if (duration == null) {
        duration = -1;
    }

    var url = "http://localhost:51901/api/bankAccountsApi/OpenNewBankAccount/?username=" + username + "&balance=" + balance + "&currency=" + currency + "&type=" + type + "&from=" + from + "&duration=" + duration;

    $.getJSON(url, function (data) {
        alert("Account Opened");
    });
}

function getBankAccounts() {
    var username = $("#hiddenUsername").val();
    var id = 1; //From Savings

    if ((username != "") && (id > 0)) {
        var url = "http://localhost:51901/api/BankAccountsApi/GetBankAccounts/?username=" + username + "&id=" + id;

        $.getJSON(url, function (data) {

            var table = "";

            if (data.length == 0) {
                table = "<p>No Bank Accounts To Transfer From</p>";
            }
            else {
                table = "<select id=\"ddlAccounts\" class=\"form-control\">";

                table += "<option value=\"-1\">Select Account</option>";

                $.each(data, function (key, val) {
                    table += "<option value=\"" + val.Iban + "\">" + val.Iban + " (" + val.Currency + val.Balance + ")" + "</option>";
                });
                table += "</select>";
            }
            $('#divFrom').html(table);
        });
    }
}

function getDuration() {
    var id = $("#ddlAccountTypes").val();

    if (id == 3) {
        var table = "<select id=\"ddlDuration\" class=\"form-control\">";
        table += "<option value=\"-1\">Select duration of months</option>";

        table += "<option value=\"1\">1</option>";
        table += "<option value=\"3\">3</option>";
        table += "<option value=\"6\">6</option>";
        table += "<option value=\"12\">12</option>";

        table += "</select>";
        $('#divDuration').html(table);
    }
}