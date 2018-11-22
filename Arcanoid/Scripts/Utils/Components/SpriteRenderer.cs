using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid.Components
{ 
    /// <summary>
    /// Component class taking care of drawing texture it contains
    /// </summary>
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

        /// <summary>
        /// Method drawing sprite on the screen. Does nothing if sprite batch or texture doesn't exist
        /// </summary>
        public void DrawSprite()
        {
            if(SpriteBatch!=null && Sprite!=null)
                SpriteBatch.Draw(Sprite, GetRectangle(), Color);
        }

        /// <summary>
        /// Function calculating texture rectangle on the scene
        /// </summary>
        /// <returns>rectangle fitting texture on the scene</returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)transform.Position.X - GetWidth() / 2 , //Position X
                                 (int)transform.Position.Y - GetHeight() / 2 , //Position Y
                                 GetWidth(), GetHeight());
        }

        /// <summary>
        /// Calculate scene width of the texture
        /// </summary>
        /// <returns>scaled texture width</returns>
        public int GetWidth()
        {
            return (int)(Sprite.Bounds.Width * transform.Scale.X);
        }

        /// <summary>
        /// Calculate scene height of the texture
        /// </summary>
        /// <returns>scaled texture height</returns>
        public int GetHeight()
        {
            return (int)(Sprite.Bounds.Height * transform.Scale.Y);
        }

    }
}
