using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid {

    public class Brick : DrawableEntity, IPhysicsEntity
    {

        public Brick(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Brick";
        }

        #region Physics

        public Rectangle GetBody()
        {
            return SpriteRenderer.GetRectangle();
        }

        public void OnCollision(IPhysicsEntity collider)
        {
            if (collider is Ball)
            {
                Destroy();
            }
        }

        #endregion

    }
}