"use strict"


const connection = new signalR.HubConnectionBuilder().withUrl("/boardHub").build();

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("Notification", function (level, message) {
    showNotification(level, message);
});
connection.on("RowUpdate", function (index, row) {
    updateRow(index, row);
});

document.getElementById("Create").addEventListener("click", function () {
    connection.invoke(
        "ReceiveAddRow",
        board,
        document.getElementById("RowName").value
    ).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("RowName").value = "";
});

let alertPlaceHolder = document.getElementById("alert-placeholder");

function showNotification(level, text) {
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

let rows = document.getElementById("rows");

function updateRow(index, row) {
    console.log(row);
    console.log(`Index: ${index}, length: ${rows.childElementCount}`);

    if (index >= rows.childElementCount) {
        console.log("Adding row...")
        let tr = document.createElement("tr");

        let th = document.createElement("th");
        th.setAttribute("scope", "row");
        th.innerText = row.name;

        tr.appendChild(th);

        rows.appendChild(tr);
    }
}