using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
    /// <summary>
    /// Text label with fading in and fading out animation
    /// </summary>
    class FadingInOutTextLabel : TextLabel
    {
        private double deltaTime;
        private bool fadeIn;
        private double fadeTime;

        public FadingInOutTextLabel(string text, SpriteFont font, SpriteBatch spriteBatch, Vector2 position, double fadeTime) : base(text, font, spriteBatch, position)
        {
            deltaTime = 0;
            this.fadeTime = fadeTime;
            fadeIn = true;
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            AddDeltaTime(gameTime);
            ChangeTextAlpha();
            CheckDeltaTime();
        }

        private void AddDeltaTime(GameTime gameTime)
        {
            deltaTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (deltaTime > fadeTime)
                deltaTime = fadeTime;
        }

        private void ChangeTextAlpha()
        {
            float alpha = CalculateAlpha();
            Color textColor = GetColor();
            Color newColor = new Color(textColor, (float)alpha);
            SetColor(newColor);
        }

        private float CalculateAlpha()
        {
            float alpha;

            if (fadeIn)
                alpha = (float)(deltaTime / fadeTime);
            else
                alpha = 1 - (float)(deltaTime / fadeTime);

            return alpha;
        }

        private void CheckDeltaTime()
        {
            if (deltaTime == fadeTime)
            {
                fadeIn = !fadeIn;
                deltaTime = 0;
            }
        }

        #endregion
    }
}
