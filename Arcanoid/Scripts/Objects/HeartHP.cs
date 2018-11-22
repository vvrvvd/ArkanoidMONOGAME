using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arkanoid.Components;

namespace Arkanoid.GameObjects
{

    /// <summary>
    /// Class for heart hp on UI, contains life count
    /// </summary>
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

        public void AddHeart()
        {
            lifeCount++;
        }

        public void RemoveHeart()
        {
            lifeCount--;
        }

        public void SetHeart(int count)
        {
            lifeCount = count;
        }

        /// <summary>
        /// Draws number of hearts equal to life count
        /// </summary>
        /// <param name="gameTime"></param>
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
