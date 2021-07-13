
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
//Disable send button until connection is established
document.getElementById("chat-submit").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    if (`${user}` === "admin2@gmail.com") {
        var div = document.createElement("div");
        var span = document.createElement("span");
        var divcontain = document.createElement("div");

        div.id = "msg-text";

        span.className = "msg-avatar";
        div.className = "cm-msg-text";

        div.textContent = `${message}`;

        document.getElementById("cm-msg-0").appendChild(div);

       
    }
    else {
        var div = document.createElement("div");
        div.id = "msg-text-rep";
        div.className = "cm-msg-text-rep";
        div.style=""
        document.getElementById("cm-msg-0").appendChild(div);
       

        div.textContent = `${message}`;
    }
    
});

connection.start().then(function () {
    document.getElementById("chat-submit").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("chat-submit").addEventListener("click", function (event) {
   
    var message = document.getElementById("chat-input__text").value;
    
        connection.invoke("SendMessage", message).catch(function (err) {
            return console.error(err.toString());
        });

        event.preventDefault();
    
});
