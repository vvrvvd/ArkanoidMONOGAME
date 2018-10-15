using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arcanoid {

    public class Ball : Entity {

        private Rectangle screenBounds;
        private Vector2 direction = Vector2.One;
        private float speed = 250f;

        public Ball(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D texture) : base(spriteBatch, startPosition)
        {
            this.Texture = texture;
            Tag = "Ball";
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
            CheckBounds();
        }

        public override void OnCollision(Entity collider)
        {
            if (collider.Tag.Equals("Paddle"))
                BounceFromBottom();
            else if (collider.Tag.Equals("Brick"))
            {
                Vector2 dist = new Vector2((transform.position.X + Texture.Width * transform.scale.X / 2f) - (collider.transform.position.X + collider.Texture.Width * collider.transform.scale.X / 2f),
                                           (transform.position.Y + Texture.Height * transform.scale.Y / 2f) - (collider.transform.position.Y + collider.Texture.Height * collider.transform.scale.Y / 2f));
                float minDistX = (Texture.Width * transform.scale.X / 2f + collider.Texture.Width * collider.transform.scale.X / 2f - 4);

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

            if (transform.position.Y + Texture.Bounds.Height * transform.scale.Y > screenBounds.Bottom)
            {
                transform.position.Y = screenBounds.Bottom - Texture.Bounds.Height * transform.scale.Y;
                BounceFromBottom();
            }
            else if (transform.position.Y < screenBounds.Top)
            {
                transform.position.Y = screenBounds.Top;
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