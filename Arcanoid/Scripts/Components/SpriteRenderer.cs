using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{ 
    public class SpriteRenderer
    {
        public Texture2D Sprite;
        public SpriteBatch SpriteBatch;
        public Color Color;

        private Entity entity;

        public SpriteRenderer(Texture2D sprite, SpriteBatch spriteBatch, Entity entity)
        {
            this.Sprite = sprite;
            this.SpriteBatch = spriteBatch;
            this.entity = entity;
            Color = Color.White;
        }

        public void DrawSprite()
        {
            if(SpriteBatch!=null && Sprite!=null)
                SpriteBatch.Draw(Sprite, GetRectangle(), Color);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)entity.Transform.Position.X - GetWidth() / 2 , //Position X
                                 (int)entity.Transform.Position.Y - GetHeight() / 2 , //Position Y
                                 GetWidth(), GetHeight());
        }

        public int GetWidth()
        {
            return (int)(Sprite.Bounds.Width * entity.Transform.Scale.X);
        }

        public int GetHeight()
        {
            return (int)(Sprite.Bounds.Height * entity.Transform.Scale.Y);
        }

    }
}
