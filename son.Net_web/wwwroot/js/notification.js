"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/ringHub").build();
let table = document.getElementById('messagesList');
//let gif = Freezeframe(document.getElementById('bell-gif'))

connection.on("ReceiveNotifications", function (message, list) {
    
    let encodedMsg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    
    
    for (let i= 0; i < list.length; i++){
        
        let date = list[i].date;
        let splitDate = date.split(' ');
        
        let row = table.insertRow(i);
        var cell1 = row.insertCell(0);
        var cell2 = row.insertCell(1);
        var cell3 = row.insertCell(2);
        cell1.innerHTML = i;
        cell2.innerHTML = splitDate[0];
        cell3.innerHTML = splitDate[1];
        console.log(date);
    }
    notify(encodedMsg);
});

const notify = function (message) {
    if(Notification.permission === 'granted'){
        const options = {
            re
            icon : "" ,
            body : 'Une nouvelle notification vient d\'arriver.',
            silent : false
        };
       
        var notif = new Notification(message, options);
    }
   
}

connection.start().then(function () {
    if('Notification' in window){
        Notification.requestPermission()
            .then((permission) => {
                if(permission !== "granted"){
                    alert('Vous devez autoriser les notifications pour utiliser le service.')
                }

            }).catch((error) => {
            console.error(error);
        })
    }else {
        alert('Le navigateur utilisé ne supporte les notifications.')
    }
}).catch(function (err) {
    return console.error(err.toString());
});