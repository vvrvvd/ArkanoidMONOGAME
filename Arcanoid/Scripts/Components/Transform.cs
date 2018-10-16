using Microsoft.Xna.Framework;

public class Transform {

    public Vector2 position;
    public Vector2 rotation;
    public Vector2 scale;

    public Transform()
    {
        this.position = Vector2.Zero;
        this.rotation = Vector2.Zero;
        this.scale = Vector2.One;
    }

    public Transform(Vector2 position) : this()
    {
        this.position = position;
    }

    public Transform(Vector2 position, Vector2 rotation) : this(position)
    {
        this.rotation = rotation;
    }

    public Transform(Vector2 position, Vector2 rotation, Vector2 scale) : this(position, rotation)
    {
        this.scale = scale;
    }

}