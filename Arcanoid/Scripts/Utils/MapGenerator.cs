using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{

    class MapGenerator
    {
        private Game game;
        private Rectangle screenBounds;
        private SpriteBatch spriteBatch;

        public MapGenerator(Game game, SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.screenBounds = screenBounds;
        }

        public List<Brick> GenerateMap(int columns, int rows, float distX, float distY)
        {
            float scaleX = 1f;
            float scaleY = 1f;

            Texture2D brickTexture = game.Content.Load<Texture2D>("element_yellow_rectangle");
            Texture2D immortalBrickTexture = game.Content.Load<Texture2D>("element_grey_rectangle");

            float width = brickTexture.Width * scaleX + distX;
            float height = brickTexture.Height * scaleY + distY;

            float offsetX = screenBounds.Right / 2 - (columns / 2 * width) + (1 - columns % 2) * 0.5f * width;
            float offsetY = brickTexture.Height;

            List<Brick> bricks = new List<Brick>();

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows - 1; j++)
                {
                    Brick brick = new Brick(spriteBatch, new Vector2((i * width) + offsetX, ((j * height) + offsetY)), brickTexture);
                    brick.Transform.scale.X = scaleX;
                    brick.Transform.scale.Y = scaleY;
                    bricks.Add(brick);
                }

                ImmortalBrick immortalBrick = new ImmortalBrick(spriteBatch, new Vector2((i * width) + offsetX, (((rows-1) * height) + offsetY)), immortalBrickTexture);
                immortalBrick.Transform.scale.X = scaleX;
                immortalBrick.Transform.scale.Y = scaleY;
                bricks.Add(immortalBrick);

            }

            return bricks;
        }

    }
}
