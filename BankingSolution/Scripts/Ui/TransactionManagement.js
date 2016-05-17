/// <reference path="../jquery-2.2.3.js" />

$(document).ready(function () {
    getAccountTypes();
});

function getAccountTypes() {

    var url = 'http://localhost:51901/api/BankAccountsApi/GetAccountTypes'; /* retrieves a list of users in json format*/

    $.getJSON(url, function (data) {
        var table = "<select onchange=\"getBankAccounts()\" id=\"ddlAccountTypes\" class=\"form-control\">";
        table += "<option value=\"-1\">Select Account Type</option>";

        $.each(data, function (key, val) {
            table += "<option value=\"" + val.Id + "\">" + val.Name + "</option>";
        });
        table += "</select>";

        $('#divAccountTypes').html(table);
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

function getTransactions() {
    var from = $("#dateFrom").val();
    var to = $("#dateTo").val();
    var username = $("#hiddenUsername").val();
    var iban = $("#ddlAccounts").val();

    if (username != "") {
        var url = "http://localhost:51901/api/bankAccountsApi/GetTransactions/?username=" + username + "&iban=" + iban + "&from=" + from + "&to=" + to;

        $.getJSON(url, function (data) {
            if (data.length == 0) {
                $('#divResult').html("<p>No Transactions</p>");
            }
            else {
                var table = "<table class=\"table\">";
                table += "<th>Iban From</th><th>Amount</th><th>Currency</th><th>Date</th><th>Iban To</th><th>Description</th>"
                $.each(data, function (key, val) {
                    table += "<tr>";
                    table += "<td>" + val.IbanFrom + "</td>";
                    table += "<td>" + val.Amount + "</td>";
                    table += "<td>" + val.Currency + "</td>";
                    table += "<td>" + val.Date + "</td>";
                    table += "<td>" + val.IbanTo + "</td>";
                    table += "<td>" + val.Description + "</td>";
                    table += "</tr>";
                });

                table += "</table>";

                $('#divResult').html(table);
            }
        });
    }
}