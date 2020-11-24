export type CommandType = 'createobject' | 'moveplayer' | 'registerconnection';
export module WebSocketCommandFactory {
    export function generateCommandString(commandType: CommandType, ...parameters: string[]): string {
        switch (commandType) {
            case 'registerconnection': return `${commandType} userHashCode:${parameters[0]}`;
        }
        return '';
    }
}