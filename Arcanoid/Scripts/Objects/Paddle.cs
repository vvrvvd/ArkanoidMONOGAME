using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Arkanoid.Components;

namespace Arkanoid.GameObjects
{

    /// <summary>
    /// Class representing moving arkanoid paddle
    /// </summary>
    public class Paddle : DrawableEntity, IPhysicsBody {

        private const float BALL_THROW_X_NOISE = 0.25f;
        private const float PADDLE_SPEED = 400f;

        private Rectangle screenBounds;
        private Vector2 direction = Vector2.Zero;
        private float deltaTime;
        private Ball ball;

        public Paddle(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Paddle";
        }

        /// <summary>
        /// Method setting paddle restrains for moving (It cannot leave bounds - paddle stops on the edge)
        /// </summary>
        /// <param name="bounds"></param>
        public void SetBounds(Rectangle bounds)
        {
            this.screenBounds = bounds;
        }

        /// <summary>
        /// Returns current paddle bounds restrains
        /// </summary>
        /// <returns>rectangle of paddle movement bounds</returns>
        public Rectangle GetBounds()
        {
            return screenBounds;
        }


        /// <summary>
        /// Returns current paddle moving direction
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDirection()
        {
            return direction;
        }


        /// <summary>
        /// Sets ball to the paddle for future putting ball on this paddle after life loss
        /// </summary>
        /// <param name="ball"></param>
        public void SetBall(Ball ball)
        {
            this.ball = ball;
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            CheckInput();
            Move();
            CheckBounds();
        }

        private void CheckInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                direction = -Vector2.UnitX;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                direction = Vector2.UnitX;
            else
                direction = Vector2.Zero;

            if(ball!=null && ball.IsOnPaddle() && Keyboard.GetState().IsKeyDown(Keys.Space))
                ThrowBall();
        }

        private void ThrowBall()
        {
            Vector2 dir = GetRandomBallDirection();
            ball.SetDirection(dir);
            ball.SetOnPaddle(false);
        }

        private Vector2 GetRandomBallDirection()
        {
            float range = BALL_THROW_X_NOISE;
            Random random = new Random();

            float noiseX = (float)random.NextDouble() * (2 * range) - range;
            Vector2 dir = new Vector2(noiseX, (float)Math.Sqrt(1 - noiseX * noiseX));
            return dir;
        }

        private void Move()
        {
            Transform.Position += direction * PADDLE_SPEED * deltaTime;
        }

        private void CheckBounds()
        {

            if (Transform.Position.X - SpriteRenderer.GetWidth()/2 < screenBounds.Left)
            {
                Transform.Position.X = screenBounds.Left + SpriteRenderer.GetWidth()/2;
                BounceFromLeft();
            }
            else if(Transform.Position.X + SpriteRenderer.GetWidth()/2 > screenBounds.Right)
            {
                Transform.Position.X = screenBounds.Right - SpriteRenderer.GetWidth()/2;
                BounceFromRight();
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


        /// <summary>
        /// Returns texture rectangular collider (from sprite renderer)
        /// </summary>
        /// <returns>sprite renderer rectangle</returns>
        public Rectangle GetCollider()
        {
            return SpriteRenderer.GetRectangle();
        }

        /// <summary>
        /// Does nothing on collision
        /// </summary>
        /// <param name="collider"></param>
        public void OnCollision(IPhysicsBody collider)
        {
            //EMPTY
        }

        #endregion

    }

}