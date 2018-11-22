using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System;
using Arkanoid.Resources;

namespace Arkanoid.GameObjects
{
    /// <summary>
    /// Class for generating brick maps
    /// </summary>
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
            purpleTexture = game.Content.Load<Texture2D>(GameResources.BRICK_PURPLE_TEXTURE);
            redTexture = game.Content.Load<Texture2D>(GameResources.BRICK_RED_TEXTURE);
            yellowTexture = game.Content.Load<Texture2D>(GameResources.BRICK_YELLOW_TEXTURE);
            greyTexture = game.Content.Load<Texture2D>(GameResources.BRICK_GREY_TEXTURE);

            purpleBrickTextures = new Texture2D[] { yellowTexture, redTexture, purpleTexture };
            redBrickTextures = new Texture2D[] { yellowTexture, redTexture};
            yellowBrickTextures = new Texture2D[] { yellowTexture};
        }

        /// <summary>
        /// Generates map of random bricks without immortal brick
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="distX"></param>
        /// <param name="distY"></param>
        /// <returns></returns>
        public List<Brick> GenerateSimpleMap(int columns, int rows, float distX, float distY)
        {
            float scaleX = 1f;
            float scaleY = 1f;

            Texture2D brickTexture = yellowTexture;

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

                    switch (rand.Next(3))
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

                    brick.Transform.Scale.X = scaleX;
                    brick.Transform.Scale.Y = scaleY;
                    bricks.Add(brick);
                }

            }

            return bricks;
        }

        /// <summary>
        /// Generates map of random bricks with checkerboard pattern of immortal bricks in the first and the last row
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="distX"></param>
        /// <param name="distY"></param>
        /// <returns></returns>
        public List<Brick> GenerateMapWithBlocks(int columns, int rows, float distX, float distY)
        {
            float scaleX = 1f;
            float scaleY = 1f;

            Texture2D brickTexture = yellowTexture;
            Texture2D immortalBrickTexture = greyTexture;

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

                    if (((j==(rows-1)) && (i%2!=0)) || ((j == 0) && (i%2==0)))
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

                    brick.Transform.Scale.X = scaleX;
                    brick.Transform.Scale.Y = scaleY;
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
