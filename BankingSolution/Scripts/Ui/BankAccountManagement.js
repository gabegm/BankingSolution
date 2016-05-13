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
    var id = $("#ddlAccountTypes").val();

    if ((username != "") && (id > 0)) {
        var url = "http://localhost:51901/api/BankAccountsApi/GetBankAccounts/?username=" + username + "&id=" + id;

        $.getJSON(url, function (data) {
            if (data.length == 0) {
                $('#divResult').html("<p>No Accounts</p>");
            }
            else {
                var table = "<table class=\"table\">";
                table += "<th>Iban</th><th>Balance</th><th>Currency</th><th>Date Opened</td>"
                $.each(data, function (key, val) {
                    table += "<tr>";
                    table += "<td>" + val.Iban + "</td>";
                    table += "<td>" + val.Balance + "</td>";
                    table += "<td>" + val.Currency + "</td>";
                    table += "<td>" + val.DateOpened + "</td>";
                    table += "</tr>";
                });

                table += "</table>";

                $('#divResult').html(table);
            }
        });
    }
}