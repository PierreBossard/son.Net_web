"use strict";

//connexion avec le hub
const connection = new signalR.HubConnectionBuilder().withUrl("/ringHub").build();

//récupère les informations des balises suivante identifiée par leurs id.
let table = document.getElementById('messagesList');
let gif = document.getElementById('bell-gif');
let audio = document.getElementById('myAudio');

connection.on("ReceiveNotifications", function (message, list) {
    
    let encodedMsg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    
    //modifie l'image de sonnette de jpg à .gif pour l'animer
    gif.setAttribute("src", "https://cdn.dribbble.com/users/1179659/screenshots/4103621/image.gif");

    //fait l'inverse 30 seconde après
    setTimeout(() => {
        gif.setAttribute("src", "https://nsa40.casimages.com/img/2021/05/12/210512122739684312.jpg")
    }, 30000);
    
    //décomposition ligne par ligne de la liste reçu en argument pour compléter la table html
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
    }
    notify(encodedMsg);
});

//fonction qui permet d'envoyer les notifications
const notify = function (message) {
    if(Notification.permission === 'granted'){
        //option de la notification
        const options = {
            icon : "" ,
            body : 'Une nouvelle notification vient d\'arriver.',
            silent : false
        };
        var notif = new Notification(message, options);
    }
}

//function lancé lors du lancement du service
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