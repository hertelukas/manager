"use strict"


const connection = new signalR.HubConnectionBuilder().withUrl("/boardHub").build();

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
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