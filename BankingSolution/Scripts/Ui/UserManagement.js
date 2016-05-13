/// <reference path="../jquery-2.2.1.js" />

function getUsers() {
    var url = 'http://localhost:51901/api/UsersApi/get';

    $.getJSON(url, function (data) {
        var table = "<table>";

        table += "<thead>";
        table += "<tr><th>Username</th><th>First Name</th><th>Surname</th></tr>";
        table += "</thead>";

        table += "<tbody>";
        $.each(data, function (key, val) {
            table += "<tr>";
            table += "<tr>" + val.Username + "</td>";
            table += "<td>" + val.FirstName + "</td>";
            table += "<td>" + val.Surname + "</td>";
            table += "</tr>";
        });

        table += "</tbody>";
        table += "</table>";

        $('#divResult').html(table);
    });
}