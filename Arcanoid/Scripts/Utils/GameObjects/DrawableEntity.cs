using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arkanoid.Components;

namespace Arkanoid
{
    /// <summary>
    /// Class for entities with textures
    /// </summary>
    public class DrawableEntity : Entity, IDrawable {

        public SpriteRenderer SpriteRenderer;

        public DrawableEntity(Texture2D sprite, SpriteBatch spriteBatch, Vector2 position)
        {
            this.Transform = new Transform(position);
            this.SpriteRenderer = new SpriteRenderer(sprite, spriteBatch, Transform);
        }

        /// <summary>
        /// Method drawing texture from sprite renderer on the screen
        /// </summary>
        /// <param name="gameTime">object containing game time passed from class Game</param>
        public virtual void Draw(GameTime gameTime)
        {
            SpriteRenderer.DrawSprite();
        }
    }
}