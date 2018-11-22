using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
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

        public void SetText(string text)
        {
            this.text = text;
            textSize = font.MeasureString(text);
        }

        public string GetText()
        {
            return text;
        }

        public Color GetColor()
        {
            return color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public Vector2 GetTextSize()
        {
            return textSize;
        }
    }
}
