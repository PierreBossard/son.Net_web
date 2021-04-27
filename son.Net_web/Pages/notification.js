var connection = new signalR.HubConnectionBuilder().withUrl("/ringHub").build();


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


connection.on("NotificationMessage", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    connection.invoke("NotificationMessage").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
