export type CommandType = 'createobject' | 'moveplayer' | 'registerconnection';
export type StringOrNumber = 'string' | 'number';

export module WebSocketCommandFactory {
    export function generateCommandString(commandType: CommandType, ...parameters: any): string {
        switch (commandType) {
            case 'registerconnection': return `${commandType} userHashCode:${parameters[0]}`;
            case 'moveplayer': return `${commandType} x:${parameters[0]} y:${parameters[1]} hashCode:${parameters[2]}`;
        }
        return '';
    }
}