import * as PIXI from "pixi.js";
export module SpriteGenerator {
    export function getSprite(x: number, y: number, assetName: string) {
        const object = new PIXI.Sprite(PIXI.Texture.from(`assets/${assetName}`));
        object.scale.set(1);
        object.x = x;
        object.y = y;

        return object;
    }
}
export default SpriteGenerator