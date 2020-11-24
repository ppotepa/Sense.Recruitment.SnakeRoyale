interface Vector2D {
    X: number;
    Y: number;
}

interface GameObject {
    HashCode: string;
    Playable: boolean;
    ObjectName?: any;
    IsSolid: boolean;
    BitmapName: string;
    Position: Vector2D;
    Velocity: Vector2D;
    Rotation: number;
    Scale: number;
    ObjectTypeName: string;
}

interface ServerStateResponse {
    GameObjects: GameObject[],
    RemovedObjects: GameObject[],
}

export { GameObject, Vector2D, ServerStateResponse }