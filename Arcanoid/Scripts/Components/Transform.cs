using Microsoft.Xna.Framework;

public class Transform {

    public Vector2 Position;
    public Vector2 Rotation;
    public Vector2 Scale;

    public Transform()
    {
        this.Position = Vector2.Zero;
        this.Rotation = Vector2.Zero;
        this.Scale = Vector2.One;
    }

    public Transform(Vector2 position) : this()
    {
        this.Position = position;
    }

    public Transform(Vector2 position, Vector2 rotation) : this(position)
    {
        this.Rotation = rotation;
    }

    public Transform(Vector2 position, Vector2 rotation, Vector2 scale) : this(position, rotation)
    {
        this.Scale = scale;
    }

}