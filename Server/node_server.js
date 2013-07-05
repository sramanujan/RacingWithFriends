var fs = require('fs');

var app = require('http').createServer();
//var io = require('socket.io').listen(app, {'transports':['websocket', 'flashsocket']});
var io = require('socket.io').listen(app);

app.listen(8028);

var globalRoom = "globalRoom";

io.on('connection', function (socket) {

    console.log("Connection initiated.....");

    socket.handlers = {
        entered_lobby: function(data) {
            socket.join(globalRoom);
            socket.sendMessage('joining_lobby', {numberOfMembers: io.sockets.clients(globalRoom).length});
            socket.sendBroadcast('member_joined_lobby', {dummydata: 'somedummy'});
        },
        start_game: function(data) {
            socket.sendBroadcastAll('game_starting', {timestamp: 'random'});
        }
    };

    socket.handleIncoming = function(eventName, data) {
        console.log("Handle Incoming - " + eventName);
        if(socket.handlers[eventName]) {
            socket.handlers[eventName](data);
        } else {
            console.log("Trying to handle an event which was not expected!");
        }
    };

    socket.sendMessage = function(eventName, data) {
        socket.emit('GameMessage', {eventName: eventName, data: data});
    };

    socket.sendBroadcast = function(eventName, data) {
		socket.broadcast.to(globalRoom).emit('GameMessage', {eventName: eventName, data: data});
    };

    socket.sendBroadcastAll = function(eventName, data) {
        io.sockets.in(globalRoom).emit('GameMessage', {eventName: eventName, data: data});
    };

    socket.on('message', function (message) {
        if(message.name == 'GameMessage') {
            socket.handleIncoming(message.args.eventName, message.args.data);
        } else {
            console.log("Received non-standard message from Client!");
            console.log(message);
        }
    });

    socket.on('GameMessage', function(data) {
        console.log("Got GameMessage through emit");
        socket.handleIncoming(data.eventName, data.data);
    });

    socket.on('disconnect', function () {
        console.log("disconnect... " );
    });

});
