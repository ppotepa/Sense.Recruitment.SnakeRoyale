import CommandSocketModule from "./modules/webSocket/CommandSocketModule"
import { GameObject, ServerStateResponse } from "./interfaces/ServerStateResponse";
import { SpriteGenerator } from "./world/SpriteGenerator";
import * as PIXI from "pixi.js";
import "./style.css";

const gameWidth = 1920;
const gameHeight = 1080;

const app = new PIXI.Application({
    backgroundColor: 0xd3d3d3,
    width: gameWidth,
    height: gameHeight,
});

const stage = app.stage;

window.onload = async (): Promise<void> => {
    document.body.appendChild(app.view);
    resizeCanvas();
};

function resizeCanvas(): void {
    const resize = () => {
        app.renderer.resize(window.innerWidth, window.innerHeight);
        app.stage.scale.x = window.innerWidth / gameWidth;
        app.stage.scale.y = window.innerHeight / gameHeight;
    };
    resize();
    window.addEventListener("resize", resize);
}

const allObjects: { [Key: string]: PIXI.Sprite; } = {};

const validateObjectExistence = (object: GameObject) => {
    if (allObjects[object.HashCode] === undefined) {
        const newObject = SpriteGenerator.getSprite(object.Position.X, object.Position.Y, object.BitmapName);
        allObjects[object.HashCode] = newObject;
        stage.addChild(newObject);
    }
    else {
        allObjects[object.HashCode].position.x = object.Position.X;
        allObjects[object.HashCode].position.y = object.Position.Y;
    }
};

const removeObject = (object: GameObject) => {
    stage.removeChild(allObjects[object.HashCode]);
    delete allObjects[object.HashCode]
    console.log(`removed object ${object.HashCode}`);
};

CommandSocketModule.start();
CommandSocketModule.addServerTickHandler((event) => {
    let state: ServerStateResponse = <ServerStateResponse>JSON.parse(event.data);
    state.RemovedObjects.forEach(removeObject);
    state.GameObjects.forEach(validateObjectExistence);
});

document.onkeypress = (ev: KeyboardEvent) => {
    console.log(ev);
    CommandSocketModule.sendKeyPressedMessage(ev.key)
}


