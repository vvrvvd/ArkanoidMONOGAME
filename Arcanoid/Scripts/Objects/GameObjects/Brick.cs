using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid {

    public class Brick : DrawableEntity, IPhysicsBody
    {
        public const int IMMORTAL_BRICK_LIFE = -1;

        private int lifeCount;
        private Texture2D[] sprites;

        public Brick(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite, int lifeCount = 1) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Brick";
            this.lifeCount = lifeCount;
        }

        public Brick(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D[] sprites, int lifeCount = 1) : base(sprites[0], spriteBatch, startPosition)
        {
            Tag = "Brick";
            this.lifeCount = (lifeCount != sprites.Length) ? sprites.Length : lifeCount;
            this.sprites = sprites;

            UpdateSpriteState();
        }

        #region Physics

        public Rectangle GetBody()
        {
            return SpriteRenderer.GetRectangle();
        }

        public virtual void OnCollision(IPhysicsBody collider)
        {
            if (collider is Ball && lifeCount > 0) //If not immortal brick
                TakeLifeCount();
        }

        private void TakeLifeCount()
        {
            lifeCount--;

            if (lifeCount <= 0)
                Destroy();
            else
                UpdateSpriteState();
        }

        private void UpdateSpriteState()
        {
            SpriteRenderer.sprite = sprites[lifeCount - 1];
        }

        #endregion

    }
}