function Validate() {
    var username, password, name, surname, email;

    username = document.getElementById("txtUsername").value;
    password = document.getElementById("txtUsername").value;
    name = document.getElementById("txtUsername").value;
    surname = document.getElementById("txtUsername").value;
    email = document.getElementById("txtUsername").value;

    if (username.length == 0) {
        document.getElementById("lblUsername").innerHTML = "Username field is empty";
    }

    if (password.length == 0) {
        document.getElementById("lblPassword").innerHTML = "Password field is empty";
    }

    if (name.length == 0) {
        document.getElementById("lblName").innerHTML = "Name field is empty";
    }

    if (surname.length == 0) {
        document.getElementById("lblSurname").innerHTML = "Surname field is empty";
    }

    if (email.length == 0) {
        document.getElementById("lblEmail").innerHTML = "Email field is empty";
    }

    if (username.length == 0 || password.length == 0 || name.length == 0 || surname.length == 0 || email.length == 0) {
        alert("You left some out some fields!");
        return false;
    }

    return true;
}