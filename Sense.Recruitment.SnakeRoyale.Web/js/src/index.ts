import CommandSocketModule from "./modules/webSocket/CommandSocketModule"
import * as PIXI from "pixi.js";
import "./style.css";
import { HashedGameObject } from "./interfaces/EngineInterfaces";

const gameWidth = 1000;
const gameHeight = 800;

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

function getObject(x: number, y: number, bitmapName: string): PIXI.Sprite {
    const apple = new PIXI.Sprite(PIXI.Texture.from("assets/apple.png"));
    apple.scale.set(1);
    apple.x = x;
    apple.y = y;
    return apple;
}

var allObjects: { [Key: string]: PIXI.Sprite; } = {};
CommandSocketModule.start();
CommandSocketModule.addServerTickHandler((event) => {
    debugger;
    let objects: HashedGameObject[] = <HashedGameObject[]>JSON.parse(event.data);
    objects.forEach(object => {
        if (allObjects[object.Key] === undefined) {
            let newObject = getObject(object.Value.Position.X, object.Value.Position.Y, object.Value.ObjectTypeName);
            stage.addChild(newObject);
        }
        else {
            allObjects[object.Key].position.x = object.Value.Position.X;
            allObjects[object.Key].position.y = object.Value.Position.Y;
        }
    });
});