"use strict"


const connection = new signalR.HubConnectionBuilder().withUrl("/boardHub").build();

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("Notification", function (level, message) {
    showNotification(level, message);
});

document.getElementById("Create").addEventListener("click", function () {
    connection.invoke(
        "ReceiveAddRow",
        board,
        document.getElementById("RowName").value
    ).catch(function (err) {
        return console.error(err.toString());
    });
});

let alertPlaceHolder = document.getElementById("alert-placeholder");

function showNotification(level, text) {
    console.log("gay");
    let alert = document.createElement("div");

    alert.className = `alert alert-${level} alert-dismissible`;
    alert.setAttribute("role", "alert");

    let button = document.createElement("button");
    button.className = "btn-close"
    button.setAttribute("type", "button");
    button.setAttribute("data-bs-dismiss", "alert");
    button.setAttribute("aria-label", "Close");

    alert.appendChild(button);
    alert.innerHTML += text;

    alertPlaceHolder.appendChild(alert);
}