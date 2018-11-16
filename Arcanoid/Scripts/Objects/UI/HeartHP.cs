using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
    class HeartHP : DrawableEntity
    {
        private const int SPACE_X = 5;

        private int lifeCount;
        private Vector2 startPosition;

        public HeartHP(Texture2D heartTexture, SpriteBatch spriteBatch, int lifeCount, Vector2 position) : base(heartTexture, spriteBatch, position)
        {
            startPosition = position;
            this.lifeCount = lifeCount;
        }

        public int GetLifeCount()
        {
            return lifeCount;
        }

        public void RemoveHeart()
        {
            lifeCount--;
        }

        public void AddHeart()
        {
            lifeCount++;
        }

        public override void Draw(GameTime gameTime)
        {
            for(int i=0; i<lifeCount; i++)
            {
                SpriteRenderer.DrawSprite();
                Transform.Position.X += (SpriteRenderer.GetWidth() + SPACE_X);
            }

            Transform.Position = startPosition;
        }

    }
}
