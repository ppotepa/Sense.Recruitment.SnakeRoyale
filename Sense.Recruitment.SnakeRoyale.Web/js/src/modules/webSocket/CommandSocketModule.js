"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CommandSocketModule = void 0;
const WebSocketCommandFactory_1 = require("../../factories/WebSocketCommandFactory");
var CommandSocketModule;
(function (CommandSocketModule) {
    const webSocketCommandUrl = "ws://localhost:2137/command";
    let socket;
    CommandSocketModule.generateUserHash = () => Math.random().toString(36).substring(2);
    const currentConnextionHash = CommandSocketModule.generateUserHash();
    function addServerTickHandler(handler) {
        socket.onmessage = handler;
    }
    CommandSocketModule.addServerTickHandler = addServerTickHandler;
    function sendKeyPressedMessage(keyPressed) {
        switch (keyPressed) {
            case 'w': socket.send(WebSocketCommandFactory_1.WebSocketCommandFactory.generateCommandString('moveplayer', 0, -32, currentConnextionHash));
            case 's': socket.send(WebSocketCommandFactory_1.WebSocketCommandFactory.generateCommandString('moveplayer', 0, 32, currentConnextionHash));
            case 'a': socket.send(WebSocketCommandFactory_1.WebSocketCommandFactory.generateCommandString('moveplayer', -32, 0, currentConnextionHash));
            case 'd': socket.send(WebSocketCommandFactory_1.WebSocketCommandFactory.generateCommandString('moveplayer', 32, 0, currentConnextionHash));
        }
    }
    CommandSocketModule.sendKeyPressedMessage = sendKeyPressedMessage;
    function start() {
        socket = new WebSocket(webSocketCommandUrl);
        socket.onopen = function (e) {
            const command = WebSocketCommandFactory_1.WebSocketCommandFactory.generateCommandString('registerconnection', currentConnextionHash);
            socket.send(command);
        };
        socket.onmessage = function (event) {
            let objects = JSON.parse(event.data);
            return objects;
        };
        socket.onclose = function (event) {
            if (event.wasClean) {
                console.log(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
            }
            else {
                console.log('[close] Connection died');
            }
        };
        socket.onerror = function (error) {
            console.log(`[error] ${error}`);
        };
    }
    CommandSocketModule.start = start;
})(CommandSocketModule = exports.CommandSocketModule || (exports.CommandSocketModule = {}));
exports.default = CommandSocketModule;
//# sourceMappingURL=CommandSocketModule.js.map