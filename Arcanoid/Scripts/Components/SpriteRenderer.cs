using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{ 
    public class SpriteRenderer
    {
        public Texture2D Sprite;
        public SpriteBatch SpriteBatch;
        public Color Color;

        private Transform transform;

        public SpriteRenderer(Texture2D sprite, SpriteBatch spriteBatch, Transform transform)
        {
            this.Sprite = sprite;
            this.SpriteBatch = spriteBatch;
            this.transform = transform;
            Color = Color.White;
        }

        public void DrawSprite()
        {
            if(SpriteBatch!=null && Sprite!=null)
                SpriteBatch.Draw(Sprite, GetRectangle(), Color);
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle((int)transform.Position.X - GetWidth() / 2 , //Position X
                                 (int)transform.Position.Y - GetHeight() / 2 , //Position Y
                                 GetWidth(), GetHeight());
        }

        public int GetWidth()
        {
            return (int)(Sprite.Bounds.Width * transform.Scale.X);
        }

        public int GetHeight()
        {
            return (int)(Sprite.Bounds.Height * transform.Scale.Y);
        }

    }
}
