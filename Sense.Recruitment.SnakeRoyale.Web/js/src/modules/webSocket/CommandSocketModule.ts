import { WebSocketCommandFactory } from "../../factories/WebSocketCommandFactory"
import { GameObject } from "../../interfaces/ServerStateResponse"
export module CommandSocketModule {
    const webSocketCommandUrl: string = "ws://localhost:2137/command"
    let socket: WebSocket;

    export const generateUserHash = () => Math.random().toString(36).substring(2);
    const currentConnextionHash = generateUserHash();

    export function addServerTickHandler(handler: ((this: WebSocket, ev: MessageEvent<string>) => any) | null): void {
        socket.onmessage = handler;
    }

    export function sendKeyPressedMessage(keyPressed: string) {
        switch (keyPressed) {
            case 'w': socket.send(WebSocketCommandFactory.generateCommandString('moveplayer', 0, -32, currentConnextionHash));
            case 's': socket.send(WebSocketCommandFactory.generateCommandString('moveplayer', 0, 32, currentConnextionHash));
            case 'a': socket.send(WebSocketCommandFactory.generateCommandString('moveplayer', -32, 0, currentConnextionHash));
            case 'd': socket.send(WebSocketCommandFactory.generateCommandString('moveplayer', 32, 0, currentConnextionHash));
        }
    }

    export function start(): void {
        socket = new WebSocket(webSocketCommandUrl);
        socket.onopen = function (e) {
            const command = WebSocketCommandFactory.generateCommandString('registerconnection', currentConnextionHash);
            socket.send(command);
        };

        socket.onmessage = function (event): GameObject[] {
            let objects = <GameObject[]>JSON.parse(event.data);
            return objects;
        };

        socket.onclose = function (event) {
            if (event.wasClean) {
                console.log(`[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`);
            } else {
                console.log('[close] Connection died');
            }
        };

        socket.onerror = function (error) {
            console.log(`[error] ${error}`);
        };
    }
}
export default CommandSocketModule;