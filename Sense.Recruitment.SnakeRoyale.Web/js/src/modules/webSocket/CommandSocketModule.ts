import { WebSocketCommandFactory } from "../../factories/WebSocketCommandFactory"
import { GameObject, Vector2D } from "../../interfaces/EngineInterfaces"
export module CommandSocketModule {
    const webSocketCommandUrl: string = "ws://localhost:2137/command"
    let socket: WebSocket;
    const generateUserHash = () => Math.random().toString(36).substring(2);

    export function addServerTickHandler(handler: ((this: WebSocket, ev: MessageEvent<string>) => any) | null): void {
        socket.onmessage = handler;
    }

    export function start(): void {
        socket = new WebSocket(webSocketCommandUrl);
        socket.onopen = function (e) {
            var command = WebSocketCommandFactory.generateCommandString('registerconnection', generateUserHash());
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
                // e.g. server process killed or network down
                // event.code is usually 1006 in this case
                console.log('[close] Connection died');
            }
        };

        socket.onerror = function (error) {
            console.log(`[error] ${error}`);
        };
    }
}
export default CommandSocketModule;