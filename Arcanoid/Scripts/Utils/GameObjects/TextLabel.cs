using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arkanoid.Components;

namespace Arkanoid
{
    /// <summary>
    /// Class for entities with text to draw
    /// </summary>
    class TextLabel : Entity, IDrawable
    {
        private SpriteFont font;
        private string text;
        private Color color;
        private SpriteBatch spriteBatch;
        private Vector2 textSize;

        public TextLabel(string text, SpriteFont font, SpriteBatch spriteBatch, Vector2 position) : base(position)
        {
            this.font = font;
            this.spriteBatch = spriteBatch;
            color = Color.White;
            SetText(text);
        }

        public TextLabel(string text, SpriteFont font, SpriteBatch spriteBatch, Transform transform)
        {
            this.font = font;
            this.spriteBatch = spriteBatch;
            this.Transform = transform;
            color = Color.White;
            SetText(text);
        }

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.DrawString(font, text, Transform.Position-textSize/2, color);
        }

        /// <summary>
        /// Set text to display
        /// </summary>
        /// <param name="text">text to displat</param>
        public void SetText(string text)
        {
            this.text = text;
            textSize = font.MeasureString(text);
        }

        /// <summary>
        /// Currently displayed text
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return text;
        }

        /// <summary>
        /// Current font color
        /// </summary>
        /// <returns>font color</returns>
        public Color GetColor()
        {
            return color;
        }

        /// <summary>
        /// Set font color
        /// </summary>
        /// <param name="color">new font color</param>
        public void SetColor(Color color)
        {
            this.color = color;
        }

        /// <summary>
        /// Calculate current scene text real size
        /// </summary>
        /// <returns>text size</returns>
        public Vector2 GetTextSize()
        {
            return textSize;
        }
    }
}
