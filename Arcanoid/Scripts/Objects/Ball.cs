using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Arkanoid.Components;

namespace Arkanoid.GameObjects
{

    /// <summary>
    /// Class representing bouncing arkanoid ball in the game
    /// </summary>
    public class Ball : DrawableEntity, IPhysicsBody {

        private const float BALL_SPEED = 350f;

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

        /// <summary>
        /// Method setting ball restrains for moving (It cannot leave bounds, also bounces of them)
        /// </summary>
        /// <param name="bounds"></param>
        public void SetBounds(Rectangle bounds)
        {
            this.screenBounds = bounds;
        }

        /// <summary>
        /// Returns current ball bounds restrains
        /// </summary>
        /// <returns>rectangle of ball movement bounds</returns>
        public Rectangle GetBounds()
        {
            return screenBounds;
        }

        /// <summary>
        /// Returns true if ball if set on paddle (after life loss)
        /// </summary>
        /// <returns></returns>
        public bool IsOnPaddle()
        {
            return isOnPaddle;
        }

        /// <summary>
        /// Sets paddle on which the ball stays after life loss
        /// </summary>
        /// <param name="state"></param>
        public void SetOnPaddle(bool state)
        {
            isOnPaddle = state;
        }

        /// <summary>
        /// Set ball moving direction
        /// </summary>
        /// <param name="direction"></param>
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
            Transform.Position += direction * BALL_SPEED * deltaTime;
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

        /// <summary>
        /// Texture rectangular collider (from sprite renderer)
        /// </summary>
        /// <returns></returns>
        public Rectangle GetCollider()
        {
            return SpriteRenderer.GetRectangle();
        }

        /// <summary>
        /// If collider is paddle - bounces with friction from paddle
        /// If collider is brick - bounces with rebound angle
        /// </summary>
        /// <param name="collider"></param>
        public void OnCollision(IPhysicsBody collider)
        {
            if (collider is Paddle)
            {
                Paddle paddle = collider as Paddle;
                Rectangle paddleBody = paddle.GetCollider();
                Vector2 paddleDirection = paddle.GetDirection();
                Rectangle ballBody = GetCollider();

                int distY = (int)Math.Ceiling(Math.Abs((ballBody.Center.Y - BALL_SPEED * direction.Y * deltaTime) - paddleBody.Center.Y));
                int minDistY = ballBody.Height / 2 + paddleBody.Height / 2;

                if (distY >= minDistY) //Not ball loose so we can bounce from paddle
                {
                    Transform.Position -= BALL_SPEED * direction * deltaTime;
                    BounceFromMovingVertCollider(collider, paddleDirection.X);
                }
            } else if (collider is Brick)
            {
                Transform.Position -= BALL_SPEED * direction * deltaTime;
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
            Rectangle colliderBody = collider.GetCollider();
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
                Rectangle colliderBody = collider.GetCollider();

                double offsetX = ((Transform.Position.X) - (colliderBody.Center.X)) / colliderBody.Width+ 0.5;
                double rad = Math.PI * offsetX/3;

                direction.X = (float)Math.Sin(rad)*verticalDirection;
                direction.Y = (float)Math.Cos(rad)*(-1);

            }
            
        }

        #endregion
    }

}