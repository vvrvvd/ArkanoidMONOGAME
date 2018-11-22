using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arkanoid.Components;

namespace Arkanoid.GameObjects
{

    /// <summary>
    /// Class for destructable arkanoid brick
    /// </summary>
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
        /// <summary>
        /// Texture rectangular collider (from sprite renderer)
        /// </summary>
        /// <returns></returns>
        public Rectangle GetCollider()
        {
            return SpriteRenderer.GetRectangle();
        }

        /// <summary>
        /// If ball is collider - takes life from brick
        /// </summary>
        /// <param name="collider"></param>
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
            SpriteRenderer.Sprite = sprites[lifeCount - 1];
        }

        #endregion

    }
}