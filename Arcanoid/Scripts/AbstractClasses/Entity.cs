using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Entity {

    public Transform Transform;
    public string Tag;

    private bool isDestroyed;

    public Entity()
    {
        this.Transform = new Transform(Vector2.Zero);
    }

    public Entity(Vector2 position)
    {
        this.Transform = new Transform(position);
    }

    public virtual void Update(GameTime gameTime)
    {
        //Dummy
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }

    public void Destroy()
    {
        isDestroyed = true;
    }
}