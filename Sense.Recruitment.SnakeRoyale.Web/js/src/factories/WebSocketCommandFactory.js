"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.WebSocketCommandFactory = void 0;
var WebSocketCommandFactory;
(function (WebSocketCommandFactory) {
    function generateCommandString(commandType, ...parameters) {
        switch (commandType) {
            case 'registerconnection': return `${commandType} userHashCode:${parameters[0]}`;
        }
        return '';
    }
    WebSocketCommandFactory.generateCommandString = generateCommandString;
})(WebSocketCommandFactory = exports.WebSocketCommandFactory || (exports.WebSocketCommandFactory = {}));
//# sourceMappingURL=WebSocketCommandFactory.js.map