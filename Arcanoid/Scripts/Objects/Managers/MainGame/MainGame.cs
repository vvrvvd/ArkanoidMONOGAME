using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    public class MainGame : Scene
    {
        MapGenerator mapGenerator;

        private Rectangle screenBounds;

        private Ball ball;
        private Paddle paddle;
        private HeartHP hp;
        private Texture2D background;

        public MainGame(Game game) : base(game)
        {
            //DUMMY
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CheckBallLoss();
        }

        private void CheckBallLoss()
        {
            if (hp.GetLifeCount() > 0 && ball.Transform.Position.Y == screenBounds.Bottom - ball.SpriteRenderer.GetHeight() / 2)
            {
                hp.RemoveHeart();
                ball.SetOnPaddle(true);

                if (hp.GetLifeCount() == 0)
                    DestroyEntity(ball);
            }
        }

        #endregion

        #region Draw

        public override void Draw(GameTime gameTime)
        {
            DrawBackground();
            base.Draw(gameTime);
        }

        private void DrawBackground()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.End();
        }

        #endregion

        #region Initialize

        public override void Initialize()
        {
            LoadTextures();

            InitializeScreenBounds();

            InitializePaddle();
            InitializeBall();
            InitializeHeartHP();

            InitializeMapGenerator();
            GenerateMap();

            entities = entitiesManager.GetEntities();
        }

        private void LoadTextures()
        {
            background = game.Content.Load<Texture2D>("background");
            game.Content.Load<Texture2D>("heart");
            game.Content.Load<Texture2D>("ballGrey");
            game.Content.Load<Texture2D>("paddle");
            game.Content.Load<Texture2D>("element_red_rectangle");
            game.Content.Load<Texture2D>("element_yellow_rectangle");
            game.Content.Load<Texture2D>("element_blue_rectangle");
            game.Content.Load<Texture2D>("element_purple_rectangle");
            game.Content.Load<Texture2D>("element_green_rectangle");
            game.Content.Load<Texture2D>("element_grey_rectangle");
        }

        private void InitializeScreenBounds()
        {
            screenBounds = game.GraphicsDevice.PresentationParameters.Bounds;
        }

        private void InitializeBall()
        {
            Texture2D ballTexture = game.Content.Load<Texture2D>("ballGrey");
            Vector2 position = new Vector2(screenBounds.Right / 2f, screenBounds.Bottom/2f);
            ball = new Ball(spriteBatch, position, ballTexture, paddle);
            ball.Transform.Scale.X = 1f;
            ball.Transform.Scale.Y = 1f;
            ball.SetBounds(screenBounds);
            ball.SetOnPaddle(true);
            entitiesManager.AddEntity(ball);
            physicsManager.AddPhysicsEntity(ball);
            paddle.SetBall(ball);
        }

        private void InitializePaddle()
        {
            float scaleX = 1f;
            float scaleY = 1f;
            Texture2D paddleTexture = game.Content.Load<Texture2D>("paddle");
            Vector2 position = new Vector2(screenBounds.Right / 2f, screenBounds.Bottom-paddleTexture.Height/2 * scaleY);
            paddle = new Paddle(spriteBatch, position, paddleTexture);
            paddle.Transform.Scale.X = scaleX;
            paddle.Transform.Scale.Y = scaleY;
            paddle.SetBounds(screenBounds);
            entitiesManager.AddEntity(paddle);
            physicsManager.AddPhysicsEntity(paddle);
        }

        private void InitializeHeartHP()
        {
            float scaleX = 0.5f;
            float scaleY = 0.5f;
            float offsetX = 10f;
            float offsetY = 10f;

            Texture2D heartTexture = game.Content.Load<Texture2D>("heart");
            Vector2 position = new Vector2(screenBounds.Left + heartTexture.Width/2f*scaleX + offsetX, screenBounds.Top + heartTexture.Height/2f * scaleY + offsetY);
            hp = new HeartHP(heartTexture, spriteBatch, 3, position);
            hp.Transform.Scale.X = scaleX;
            hp.Transform.Scale.Y = scaleY;
            uiManager.AddEntity(hp);
        }

        private void InitializeMapGenerator()
        {
            mapGenerator = new MapGenerator(game, spriteBatch, screenBounds);
        }

        private void GenerateMap()
        {
            List<Brick> map = mapGenerator.GenerateSimpleMap(5, 4, 5f, 5f);

            entitiesManager.AddEntity(map);
            physicsManager.AddPhysicsEntity(map);
        }

        #endregion
    }
}
