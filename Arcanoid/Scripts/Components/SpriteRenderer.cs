using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{ 
    public class SpriteRenderer
    {
        public Texture2D sprite;
        public SpriteBatch spriteBatch;
        public Color color;

        private Entity entity;

        public SpriteRenderer(Texture2D sprite, SpriteBatch spriteBatch, Entity entity)
        {
            this.sprite = sprite;
            this.spriteBatch = spriteBatch;
            this.entity = entity;
            color = Color.White;
        }

        public void DrawSprite()
        {
            if(spriteBatch!=null && sprite!=null)
                spriteBatch.Draw(sprite, GetRectangle(), color);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)entity.Transform.position.X - GetWidth() / 2 , //Position X
                                 (int)entity.Transform.position.Y - GetHeight() / 2 , //Position Y
                                 GetWidth(), GetHeight());
        }

        public int GetWidth()
        {
            return (int)(sprite.Bounds.Width * entity.Transform.scale.X);
        }

        public int GetHeight()
        {
            return (int)(sprite.Bounds.Height * entity.Transform.scale.Y);
        }

    }
}
