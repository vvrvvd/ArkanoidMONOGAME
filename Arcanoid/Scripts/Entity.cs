using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class Entity {

    public readonly Transform transform;

    public Texture2D Texture;
    public string Tag;

    protected SpriteBatch spriteBatch;
    private bool isDestroyed;

    public Entity(SpriteBatch spriteBatch)
    {
        this.spriteBatch = spriteBatch;
    }

    public Entity(SpriteBatch spriteBatch, Vector2 position) : this(spriteBatch)
    {
        this.transform = new Transform(position);
    }

    public bool IsDestroyed()
    {
        return isDestroyed;
    }

    public void Destroy()
    {
        isDestroyed = true;
    }

    public virtual Rectangle GetRectangle()
    {
        return new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)(Texture.Bounds.Width * transform.scale.X), (int)(Texture.Bounds.Height * transform.scale.Y));
    }

    public virtual void Update(GameTime gameTime)
    {
        //Empty
    }

    public virtual void Draw()
    {
        spriteBatch.Draw(Texture, new Rectangle((int)transform.position.X, (int)transform.position.Y, (int)(Texture.Width * transform.scale.X), (int)(Texture.Height * transform.scale.Y)), Color.White);
    }

    public virtual void OnCollision(Entity collider)
    {
        //Empty
    }
}