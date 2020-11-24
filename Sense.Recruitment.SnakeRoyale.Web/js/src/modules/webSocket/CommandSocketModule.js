"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.CommandSocketModule = void 0;
const WebSocketCommandFactory_1 = require("../../factories/WebSocketCommandFactory");
var CommandSocketModule;
(function (CommandSocketModule) {
    const webSocketCommandUrl = "ws://localhost:2137/command";
    let socket;
    const generateUserHash = () => Math.random().toString(36).substring(2);
    function start() {
        socket = new WebSocket(webSocketCommandUrl);
        socket.onopen = function (e) {
            var command = WebSocketCommandFactory_1.WebSocketCommandFactory.generateCommandString('registerconnection', generateUserHash());
            socket.send(command);
        };
        socket.onmessage = function (event) {
            alert(`[message] Data received from server: ${event.data}`);
        };
        socket.onclose = function (event) {
            if (event.wasClean) {
                alert(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
            }
            else {
                // e.g. server process killed or network down
                // event.code is usually 1006 in this case
                alert('[close] Connection died');
            }
        };
        socket.onerror = function (error) {
            alert(`[error] ${error}`);
        };
    }
    CommandSocketModule.start = start;
})(CommandSocketModule = exports.CommandSocketModule || (exports.CommandSocketModule = {}));
exports.default = CommandSocketModule;
//# sourceMappingURL=CommandSocketModule.js.map