using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid {

    public class ImmortalBrick : Brick
    {
        private int lifeCount;

        public Brick(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite, int lifeCount = 1) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Brick";
            this.lifeCount = lifeCount;
        }

        #region Physics

        public Rectangle GetBody()
        {
            return SpriteRenderer.GetRectangle();
        }

        public virtual void OnCollision(IPhysicsBody collider)
        {
            if (collider is Ball)
            {
                TakeLifeCount();
            }
        }

        private void TakeLifeCount()
        {
            lifeCount--;
            if (lifeCount <= 0)
                Destroy();
        }

        #endregion

    }
}