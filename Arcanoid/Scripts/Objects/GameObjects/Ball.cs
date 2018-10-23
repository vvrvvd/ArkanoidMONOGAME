using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid {

    public class Ball : DrawableEntity, IPhysicsEntity {

        private Rectangle screenBounds;
        private Vector2 direction = Vector2.One;
        private float speed = 250f;
        private float deltaTime;

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
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Move();
            CheckBounds();
        }

        private void Move()
        {
            Transform.position += direction * speed * deltaTime;
        }

        private void CheckBounds()
        {

            if (Transform.position.X  - SpriteRenderer.GetWidth()/2 < screenBounds.Left)
            {
                Transform.position.X = screenBounds.Left + SpriteRenderer.GetWidth()/2;
                BounceFromVertical();
            }
            else if(Transform.position.X + SpriteRenderer.GetWidth()/2 > screenBounds.Right)
            {
                Transform.position.X = screenBounds.Right - SpriteRenderer.GetWidth()/2;
                BounceFromVertical();
            }

            if (Transform.position.Y + SpriteRenderer.GetHeight()/2 > screenBounds.Bottom)
            {
                Transform.position.Y = screenBounds.Bottom - SpriteRenderer.GetHeight()/2;
                BounceFromHorizontal();
            }
            else if (Transform.position.Y  - SpriteRenderer.GetHeight()/2 < screenBounds.Top)
            {
                Transform.position.Y = screenBounds.Top + SpriteRenderer.GetHeight() / 2;
                BounceFromHorizontal();
            }
        }

        #endregion

        #region Physics

        public Rectangle GetBody()
        {
            return SpriteRenderer.GetRectangle();
        }

        public void OnCollision(IPhysicsEntity collider)
        {
            if (collider is Paddle)
            {
                Rectangle colliderBody = collider.GetBody();
                Rectangle ballBody = GetBody();
                int distY = (int)Math.Ceiling(Math.Abs((ballBody.Center.Y - speed * direction.Y * deltaTime) - colliderBody.Center.Y));
                int minDistY = ballBody.Height/2 + colliderBody.Height/2;
                if (distY >= minDistY)
                {
                    Transform.position -= speed * direction * deltaTime;
                    BounceFromColliderUsingOffsetAngle(collider);
                }
            } else if (collider is Brick)
            {
                Transform.position -= speed * direction * deltaTime;
                BounceFromCollider(collider);
            }
        }

        #endregion

        #region Bounce

        private void BounceFromHorizontal()
        {
            direction = new Vector2(direction.X, -direction.Y);
        }

        private void BounceFromVertical()
        {
            direction = new Vector2(-direction.X, direction.Y);
        }

        private void BounceFromCollider(IPhysicsEntity collider)
        {
            Rectangle colliderBody = collider.GetBody();
            Vector2 dist = new Vector2((Transform.position.X) - (colliderBody.Center.X),
                                       (Transform.position.Y) - (colliderBody.Center.Y));
            float minDistX = (SpriteRenderer.GetWidth() / 2f + colliderBody.Width / 2f - 1);

            if (Math.Abs(dist.X) >= (minDistX))
            {
                BounceFromVertical();
            }
            else
            {
                BounceFromHorizontal();
            }
        }

        private void BounceFromColliderUsingOffsetAngle(IPhysicsEntity collider)
        {
            Rectangle colliderBody = collider.GetBody();
            Rectangle ballBody = GetBody();
            double offsetX = ((Transform.position.X) - (colliderBody.Center.X))/colliderBody.Width;
            double rad = Math.PI * (2 * offsetX);

            direction.X = (float)Math.Sin(rad);
            direction.Y = -(float)Math.Cos(rad);
            direction.Normalize();
            direction *= 2;
        }

        #endregion
    }

}