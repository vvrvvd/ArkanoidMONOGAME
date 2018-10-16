using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid {

    public class Paddle : DrawableEntity, IPhysicsEntity {

        private Rectangle screenBounds;
        private Vector2 direction = Vector2.Zero;
        private float speed = 400f;

        public Paddle(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite) : base(sprite, spriteBatch, startPosition)
        {
            Tag = "Paddle";
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

        private void Move(GameTime gameTime)
        {
            Transform.position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void CheckBounds()
        {

            if (Transform.position.X - SpriteRenderer.GetWidth()/2 < screenBounds.Left)
            {
                Transform.position.X = screenBounds.Left + SpriteRenderer.GetWidth()/2;
                BounceFromLeft();
            }
            else if(Transform.position.X + SpriteRenderer.GetWidth()/2 > screenBounds.Right)
            {
                Transform.position.X = screenBounds.Right - SpriteRenderer.GetWidth()/2;
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

        public Rectangle GetBody()
        {
            return SpriteRenderer.GetRectangle();
        }

        public void OnCollision(Entity collider)
        {
            
        }

        #endregion

    }

}