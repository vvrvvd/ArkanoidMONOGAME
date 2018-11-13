using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Arkanoid
{

    class MapGenerator
    {
        private Game game;
        private Rectangle screenBounds;
        private SpriteBatch spriteBatch;

        private Texture2D purpleTexture;
        private Texture2D redTexture;
        private Texture2D yellowTexture;
        private Texture2D greyTexture;

        private Texture2D[] purpleBrickTextures;
        private Texture2D[] redBrickTextures;
        private Texture2D[] yellowBrickTextures;

        public MapGenerator(Game game, SpriteBatch spriteBatch, Rectangle screenBounds)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.screenBounds = screenBounds;

            InitializeBrickTextures();
        }

        private void InitializeBrickTextures()
        {
            purpleTexture = game.Content.Load<Texture2D>("element_purple_rectangle");
            redTexture = game.Content.Load<Texture2D>("element_red_rectangle");
            yellowTexture = game.Content.Load<Texture2D>("element_yellow_rectangle");
            greyTexture = game.Content.Load<Texture2D>("element_grey_rectangle");

            purpleBrickTextures = new Texture2D[] { yellowTexture, redTexture, purpleTexture };
            redBrickTextures = new Texture2D[] { yellowTexture, redTexture};
            yellowBrickTextures = new Texture2D[] { yellowTexture};
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
            Random rand = new Random();

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Brick brick;
                    Vector2 position = new Vector2((i * width) + offsetX, ((j * height) + offsetY));

                    if (((i==0 || i==(columns-1)) || (j==0 || j == (rows-1))) && (i%2==0 && j%2==0))
                     {
                        brick = CreateImmortalBrick(position);
                    }
                    else
                    {
                        switch(rand.Next(3))
                        {
                            case 0:
                                brick = CreatePurpleBrick(position);
                                break;
                            case 1:
                                brick = CreateRedBrick(position);
                                break;
                            default:
                                brick = CreateYellowBrick(position);
                                break;
                        }
                    }

                    brick.Transform.scale.X = scaleX;
                    brick.Transform.scale.Y = scaleY;
                    bricks.Add(brick);
                }

            }

            return bricks;
        }

        private Brick CreatePurpleBrick(Vector2 position)
        {
            return new Brick(spriteBatch, position, purpleBrickTextures, 3);
        }

        private Brick CreateRedBrick(Vector2 position)
        {
            return new Brick(spriteBatch, position, redBrickTextures, 2);
        }

        private Brick CreateYellowBrick(Vector2 position)
        {
            return new Brick(spriteBatch, position, yellowBrickTextures, 1);
        }

        private Brick CreateImmortalBrick(Vector2 position)
        {
            return new Brick(spriteBatch, position, greyTexture, Brick.IMMORTAL_BRICK_LIFE);
        }
    }
}
