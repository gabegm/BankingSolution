/// <reference path="../jquery-2.2.3.js" />

$(document).ready(function () {
    getBankAccounts();

    $('.drag').draggable({
        revert: true,
        helper: 'clone'
    });

    $("#ibanTo").droppable({
        drop: function (event, ui) {
            this.value = $(ui.draggable).text();
        }
    });
});

function Transfer() {
    var username = $("#hiddenUsername").val();
    var ibanFrom = $("#ddlAccounts").val();
    var ibanTo = $("#ibanTo").val();
    var amount = $("#txtAmount").val();
    var url = "http://localhost:51901/api/BankAccountsApi/TransferFunds/?username=" + username + "&from=" + ibanFrom + "&to=" + ibanTo + "&amount=" + amount;

    $.getJSON(url, function (data) {
        alert("Transfer successful");
    })
    .error(function () { alert("not enough balance"); })
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