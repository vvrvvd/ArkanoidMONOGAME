using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid {

    public class Ball : DrawableEntity, IPhysicsBody {

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

        public void OnCollision(IPhysicsBody collider)
        {
            if (collider is Paddle)
            {
                Paddle paddle = (Paddle)collider;
                Rectangle paddleBody = paddle.GetBody();
                Vector2 paddleDirection = paddle.GetDirection();
                Rectangle ballBody = GetBody();

                int distY = (int)Math.Ceiling(Math.Abs((ballBody.Center.Y - speed * direction.Y * deltaTime) - paddleBody.Center.Y));
                int minDistY = ballBody.Height / 2 + paddleBody.Height / 2;

                if (distY >= minDistY) //Not ball loose so we can bounce from paddle
                {
                    Transform.position -= speed * direction * deltaTime;
                    BounceFromMovingVertCollider(collider, paddleDirection.X);
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

        private void BounceFromCollider(IPhysicsBody collider)
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

        private void BounceFromMovingVertCollider(IPhysicsBody collider, float verticalDirection)
        {
            if (verticalDirection == 0) //Not moving
            {
                BounceFromCollider(collider);
            }
            else //Moving to the one side -1 or 1
            {
                Rectangle colliderBody = collider.GetBody();

                double offsetX = ((Transform.position.X) - (colliderBody.Center.X)) / colliderBody.Width+ 0.5;
                double rad = Math.PI * offsetX/3;

                direction.X = (float)Math.Sin(rad)*verticalDirection;
                direction.Y = (float)Math.Cos(rad)*(-1);

            }
            
        }

        #endregion
    }

}