"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/ringHub").build();


connection.on("ReceiveNotifications", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});