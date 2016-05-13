/// <reference path="../jquery-2.2.3.js" />

$(document).ready(function () {
    
});

function getTransactions() {
    var from = $("#dateFrom").val();
    var to = $("#dateTo").val();
    var username = $("#hiddenUsername").val();

    if (username != "") {
        var url = "http://localhost:51901/api/bankAccountsApi/GetTransactions/?username=" + username + "&from=" + from + "&to=" + to;

        $.getJSON(url, function (data) {
            if (data.length == 0) {
                $('#divResult').html("<p>No Transactions</p>");
            }
            else {
                var table = "<table class=\"table\">";
                table += "<th>IbanFrom</th><th>Amount</th><th>Currency</th><th>Date</th><th>IbanTo</th><th>Description</th>"
                $.each(data, function (key, val) {
                    table += "<tr>";
                    table += "<td>" + val.Iban + "</td>";
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

function AddTransaction() {
    var iban = $("#ddlCurrencies").val();
    var date = $("#ddlAccountTypes").val();
    var amount = $("#txtAmount").val();
    var username = $("#hiddenUsername").val();
    var url = "http://localhost:51901/api/bankAccountsApi/OpenNewBankAccount/?username=" + username + "&balance=" + balance + "&currency=" + currency + "&type=" + type;

    $.getJSON(url, function (data) {
        alert("Account Opened");
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