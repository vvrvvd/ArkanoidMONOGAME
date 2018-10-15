using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid {

    public class Paddle : Entity {

        private Rectangle screenBounds;
        private Vector2 direction = Vector2.Zero;
        private float speed = 400f;

        public Paddle(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D texture) : base(spriteBatch, startPosition)
        {
            this.Texture = texture;
            Tag = "Paddle";
        }

        public override void Update(GameTime gameTime)
        {
            CheckInput();
            Move(gameTime);
            CheckBounds();
        }

        private void CheckInput()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                direction = -Vector2.UnitX;
            } else if(Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                direction = Vector2.UnitX;
            } else
            {
                direction = Vector2.Zero;
            }
        }

        private void CheckBounds()
        {

            if (transform.position.X < screenBounds.Left)
            {
                transform.position.X = screenBounds.Left;
                BounceFromLeft();
            }
            else if(transform.position.X + Texture.Bounds.Width * transform.scale.X > screenBounds.Right)
            {
                transform.position.X = screenBounds.Right - Texture.Bounds.Width * transform.scale.X;
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

        private void Move(GameTime gameTime)
        {
            transform.position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void SetBounds(Rectangle bounds)
        {
            this.screenBounds = bounds;
        }

    }

}