using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid {

    public class Ball : DrawableEntity, IPhysicsEntity {

        private Rectangle screenBounds;
        private Vector2 direction = Vector2.One;
        private float speed = 250f;

        public Ball(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Ball";
        }

        public void SetBounds(Rectangle bounds)
        {
            this.screenBounds = bounds;
        }

        public Rectangle GetBounds()
        {
            return screenBounds;
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            CheckBounds();
        }

        private void Move(GameTime gameTime)
        {
            Transform.position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void CheckBounds()
        {

            if (Transform.position.X  - SpriteRenderer.GetWidth()/2 < screenBounds.Left)
            {
                Transform.position.X = screenBounds.Left + SpriteRenderer.GetWidth()/2;
                BounceFromLeft();
            }
            else if(Transform.position.X + SpriteRenderer.GetWidth()/2 > screenBounds.Right)
            {
                Transform.position.X = screenBounds.Right - SpriteRenderer.GetWidth()/2;
                BounceFromRight();
            }

            if (Transform.position.Y + SpriteRenderer.GetHeight()/2 > screenBounds.Bottom)
            {
                Transform.position.Y = screenBounds.Bottom - SpriteRenderer.GetHeight()/2;
                BounceFromBottom();
            }
            else if (Transform.position.Y  - SpriteRenderer.GetHeight()/2 < screenBounds.Top)
            {
                Transform.position.Y = screenBounds.Top + SpriteRenderer.GetHeight() / 2;
                BounceFromTop();
            }
        }

        #region Bounce

        private void BounceFromTop()
        {
            direction = new Vector2(direction.X, -direction.Y);
        }

        private void BounceFromBottom()
        {
            direction = new Vector2(direction.X, -direction.Y);
        }

        private void BounceFromLeft()
        {
            direction = new Vector2(-direction.X, direction.Y);
        }

        private void BounceFromRight()
        {
            direction = new Vector2(-direction.X, direction.Y);
        }

        #endregion

        #endregion

        #region Physics

        public Rectangle GetBody()
        {
            return SpriteRenderer.GetRectangle();
        }

        public void OnCollision(Entity collider)
        {
            if (collider.Tag.Equals("Paddle"))
                BounceFromBottom();
            else if (collider.Tag.Equals("Brick"))
            {
                DrawableEntity drawableCollider = (DrawableEntity)collider;
                Vector2 dist = new Vector2((Transform.position.X + SpriteRenderer.GetWidth()/2f) - (drawableCollider.Transform.position.X + drawableCollider.SpriteRenderer.GetWidth()/2f),
                                           (Transform.position.Y + SpriteRenderer.GetHeight()/2f) - (drawableCollider.Transform.position.Y + drawableCollider.SpriteRenderer.GetWidth()/2f));
                float minDistX = (SpriteRenderer.GetWidth()/2f + drawableCollider.SpriteRenderer.GetWidth()/2f - 4);

                if (Math.Abs(dist.X) >= (minDistX))
                {
                    BounceFromLeft();
                }
                else
                {
                    BounceFromTop();
                }
            }
        }

        #endregion
    }

}