using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
    class FadingInOutTextLabel : Entity, IDrawable
    {
        private TextLabel textLabel;
        private double deltaTime;
        private bool fadeIn;
        private double fadeTime;

        public FadingInOutTextLabel(string text, SpriteFont font, SpriteBatch spriteBatch, Vector2 position, double fadeTime) : base(position)
        {
            textLabel = new TextLabel(text, font, spriteBatch, Transform);
            deltaTime = 0;
            this.fadeTime = fadeTime;
            fadeIn = true;
        }

        #region Draw

        public void Draw(GameTime gameTime)
        {
            textLabel.Draw(gameTime);
        }

        #endregion

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
            Color textColor = textLabel.GetColor();
            Color newColor = new Color(textColor, (float)alpha);
            textLabel.SetColor(newColor);
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
