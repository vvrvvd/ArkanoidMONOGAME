using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid {

    public class Ball : DrawableEntity, IPhysicsBody {

        private readonly float speed = 350f;

        private Rectangle screenBounds;
        private Vector2 direction;
        private float deltaTime;
        private Paddle paddle;
        private bool isOnPaddle;

        public Ball(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite, Paddle paddle) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Ball";
            direction = Vector2.One;
            direction.Normalize();
            this.paddle = paddle;
        }

        public void SetBounds(Rectangle bounds)
        {
            this.screenBounds = bounds;
        }

        public Rectangle GetBounds()
        {
            return screenBounds;
        }

        public bool IsOnPaddle()
        {
            return isOnPaddle;
        }

        public void SetOnPaddle(bool state)
        {
            isOnPaddle = state;
        }

        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(isOnPaddle)
            {
                OnPaddle();
            } else
            {
                Move();
                CheckBounds();
            }
        }

        private void OnPaddle()
        {
            float offsetY = paddle.Transform.Position.Y - SpriteRenderer.Sprite.Height * Transform.Scale.Y;
            Transform.Position = new Vector2(paddle.Transform.Position.X, offsetY);
        }

        private void Move()
        {
            Transform.Position += direction * speed * deltaTime;
        }

        private void CheckBounds()
        {

            if (Transform.Position.X  - SpriteRenderer.GetWidth()/2 < screenBounds.Left)
            {
                Transform.Position.X = screenBounds.Left + SpriteRenderer.GetWidth()/2;
                BounceFromVertical();
            }
            else if(Transform.Position.X + SpriteRenderer.GetWidth()/2 > screenBounds.Right)
            {
                Transform.Position.X = screenBounds.Right - SpriteRenderer.GetWidth()/2;
                BounceFromVertical();
            }

            if (Transform.Position.Y + SpriteRenderer.GetHeight()/2 > screenBounds.Bottom)
            {
                Transform.Position.Y = screenBounds.Bottom - SpriteRenderer.GetHeight()/2;
                BounceFromHorizontal();
            }
            else if (Transform.Position.Y  - SpriteRenderer.GetHeight()/2 < screenBounds.Top)
            {
                Transform.Position.Y = screenBounds.Top + SpriteRenderer.GetHeight() / 2;
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
                Paddle paddle = collider as Paddle;
                Rectangle paddleBody = paddle.GetBody();
                Vector2 paddleDirection = paddle.GetDirection();
                Rectangle ballBody = GetBody();

                int distY = (int)Math.Ceiling(Math.Abs((ballBody.Center.Y - speed * direction.Y * deltaTime) - paddleBody.Center.Y));
                int minDistY = ballBody.Height / 2 + paddleBody.Height / 2;

                if (distY >= minDistY) //Not ball loose so we can bounce from paddle
                {
                    Transform.Position -= speed * direction * deltaTime;
                    BounceFromMovingVertCollider(collider, paddleDirection.X);
                }
            } else if (collider is Brick)
            {
                Transform.Position -= speed * direction * deltaTime;
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
            Vector2 dist = new Vector2((Transform.Position.X) - (colliderBody.Center.X),
                                       (Transform.Position.Y) - (colliderBody.Center.Y));
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

                double offsetX = ((Transform.Position.X) - (colliderBody.Center.X)) / colliderBody.Width+ 0.5;
                double rad = Math.PI * offsetX/3;

                direction.X = (float)Math.Sin(rad)*verticalDirection;
                direction.Y = (float)Math.Cos(rad)*(-1);

            }
            
        }

        #endregion
    }

}