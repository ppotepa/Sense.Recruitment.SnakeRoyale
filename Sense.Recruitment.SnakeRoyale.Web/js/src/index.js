"use strict";
var __createBinding = (this && this.__createBinding) || (Object.create ? (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    Object.defineProperty(o, k2, { enumerable: true, get: function() { return m[k]; } });
}) : (function(o, m, k, k2) {
    if (k2 === undefined) k2 = k;
    o[k2] = m[k];
}));
var __setModuleDefault = (this && this.__setModuleDefault) || (Object.create ? (function(o, v) {
    Object.defineProperty(o, "default", { enumerable: true, value: v });
}) : function(o, v) {
    o["default"] = v;
});
var __importStar = (this && this.__importStar) || function (mod) {
    if (mod && mod.__esModule) return mod;
    var result = {};
    if (mod != null) for (var k in mod) if (k !== "default" && Object.prototype.hasOwnProperty.call(mod, k)) __createBinding(result, mod, k);
    __setModuleDefault(result, mod);
    return result;
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const CommandSocketModule_1 = __importDefault(require("./modules/webSocket/CommandSocketModule"));
const SpriteGenerator_1 = require("./world/SpriteGenerator");
const PIXI = __importStar(require("pixi.js"));
require("./style.css");
const gameWidth = 1920;
const gameHeight = 1080;
const app = new PIXI.Application({
    backgroundColor: 0xd3d3d3,
    width: gameWidth,
    height: gameHeight,
});
const stage = app.stage;
window.onload = async () => {
    document.body.appendChild(app.view);
    resizeCanvas();
};
function resizeCanvas() {
    const resize = () => {
        app.renderer.resize(window.innerWidth, window.innerHeight);
        app.stage.scale.x = window.innerWidth / gameWidth;
        app.stage.scale.y = window.innerHeight / gameHeight;
    };
    resize();
    window.addEventListener("resize", resize);
}
const allObjects = {};
const validateObjectExistence = (object) => {
    if (allObjects[object.HashCode] === undefined) {
        const newObject = SpriteGenerator_1.SpriteGenerator.getSprite(object.Position.X, object.Position.Y, object.BitmapName);
        allObjects[object.HashCode] = newObject;
        stage.addChild(newObject);
    }
    else {
        allObjects[object.HashCode].position.x = object.Position.X;
        allObjects[object.HashCode].position.y = object.Position.Y;
    }
};
const removeObject = (object) => {
    stage.removeChild(allObjects[object.HashCode]);
    delete allObjects[object.HashCode];
    console.log(`removed object ${object.HashCode}`);
};
CommandSocketModule_1.default.start();
CommandSocketModule_1.default.addServerTickHandler((event) => {
    let state = JSON.parse(event.data);
    state.RemovedObjects.forEach(removeObject);
    state.GameObjects.forEach(validateObjectExistence);
});
document.onkeypress = (ev) => {
    console.log(ev);
    CommandSocketModule_1.default.sendKeyPressedMessage(ev.key);
};
//# sourceMappingURL=index.js.map