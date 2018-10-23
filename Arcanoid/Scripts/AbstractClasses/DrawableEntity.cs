using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{

    public abstract class DrawableEntity : Entity, IDrawable {

        public SpriteRenderer SpriteRenderer;

        public DrawableEntity(Texture2D sprite, SpriteBatch spriteBatch, Vector2 position)
        {
            this.Transform = new Transform(position);
            this.SpriteRenderer = new SpriteRenderer(sprite, spriteBatch, this);
        }

        public virtual void Draw(GameTime gameTime)
        {
            SpriteRenderer.DrawSprite();
        }
    }
}