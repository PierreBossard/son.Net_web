"use strict";

const connection = new signalR.HubConnectionBuilder().withUrl("/ringHub").build();


connection.on("ReceiveNotifications", function (message, list) {
    
   
    let encodedMsg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    
    for (let i= 0; i < list.length; i++){
        let date = list[i].date;
        console.log(date);
    }
   
    notify(encodedMsg);
});

const notify = function (message) {
    if(Notification.permission === 'granted'){
        const options = {
            icon : "" ,
            body : 'Ajouter texte corps',
            silent : false
        };
        
        //TODO IMPLEMENT ACTION IN NOTIF
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