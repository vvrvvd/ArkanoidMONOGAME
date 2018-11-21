using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arkanoid
{
    public class MainGame : Scene
    {
        private static Vector2 TEXT_SPACE = new Vector2(0, 5);

        private const string GAME_OVER_TEXT = "Game Over";
        private const string RESTART_TEXT = "Press ENTER to restart";
        private const int INIT_LIFE_COUNT = 3;

        MapGenerator mapGenerator;

        private Rectangle screenBounds;

        private Ball ball;
        private Paddle paddle;
        private HeartHP hp;
        private Texture2D background;

        private SpriteFont gameOverFont;
        private SpriteFont restartFont;
        private Vector2 screenCenter;

        private DrawableEntity gameOverForeground;
        private TextLabel gameOverText;
        private TextLabel restartText;

        private bool gameOver;

        public MainGame(Game game) : base(game)
        {
            //DUMMY
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            if(!gameOver)
            {
                base.Update(gameTime);
                CheckBallLoss();
            }
            else
                CheckRestartInput();
        }

        private void CheckBallLoss()
        {
            if (hp.GetLifeCount() > 0 && ball.Transform.Position.Y == screenBounds.Bottom - ball.SpriteRenderer.GetHeight() / 2)
            {
                hp.RemoveHeart();
                ball.SetOnPaddle(true);

                if (hp.GetLifeCount() == 0)
                {
                    GameOver();
                }
            }
        }

        private void GameOver()
        {
            DestroyEntity(ball);
            managerUI.AddEntity(gameOverForeground);
            managerUI.AddEntity(gameOverText);
            managerUI.AddEntity(restartText);
            gameOver = true;
        }

        private void CheckRestartInput()
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                PrepareNewGame();
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
            LoadFonts();
            InitializeScreenBounds();

            InitializePaddle();
            InitializeBall();
            InitializeMapGenerator();

            InitializeUI();

            PrepareNewGame();

            entities = entitiesManager.GetEntities();
        }

        private void LoadTextures()
        {
            background = game.Content.Load<Texture2D>("background");
            game.Content.Load<Texture2D>("transparentForeground");
            game.Content.Load<Texture2D>("heart");
            game.Content.Load<Texture2D>("ballGrey");
            game.Content.Load<Texture2D>("paddleBlu");
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
            screenCenter = new Vector2(screenBounds.Width / 2, screenBounds.Height / 2);
        }

        private void LoadFonts()
        {
            gameOverFont = game.Content.Load<SpriteFont>("gameOverFont");
            restartFont = game.Content.Load<SpriteFont>("restartFont");
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
        }

        private void InitializePaddle()
        {
            float scaleX = 1f;
            float scaleY = 1f;
            Texture2D paddleTexture = game.Content.Load<Texture2D>("paddleBlu");
            Vector2 position = new Vector2(screenBounds.Right / 2f, screenBounds.Bottom-paddleTexture.Height/2 * scaleY);
            paddle = new Paddle(spriteBatch, position, paddleTexture);
            paddle.Transform.Scale.X = scaleX;
            paddle.Transform.Scale.Y = scaleY;
            paddle.SetBounds(screenBounds);
        }

        private void InitializeUI()
        {
            InitializeHeartHP();
            InitializeGameOverForeground();
            InitializeTexts();
        }

        private void InitializeHeartHP()
        {
            float scaleX = 0.5f;
            float scaleY = 0.5f;
            float offsetX = 10f;
            float offsetY = 10f;

            Texture2D heartTexture = game.Content.Load<Texture2D>("heart");
            Vector2 position = new Vector2(screenBounds.Left + heartTexture.Width/2f*scaleX + offsetX, screenBounds.Top + heartTexture.Height/2f * scaleY + offsetY);
            hp = new HeartHP(heartTexture, spriteBatch, 0, position);
            hp.Transform.Scale.X = scaleX;
            hp.Transform.Scale.Y = scaleY;
            managerUI.AddEntity(hp);
        }

        private void InitializeTexts()
        {
            gameOverText = new TextLabel(GAME_OVER_TEXT, gameOverFont, spriteBatch, screenCenter);

            Vector2 restartTextPosition = screenCenter + new Vector2(0, gameOverText.GetTextSize().Y) + TEXT_SPACE;
            restartText = new TextLabel(RESTART_TEXT, restartFont, spriteBatch, restartTextPosition);
        }

        private void InitializeGameOverForeground()
        {
            Texture2D foregroundTexture = game.Content.Load<Texture2D>("transparentForeground");
            gameOverForeground = new DrawableEntity(foregroundTexture, spriteBatch, screenCenter);
        }

        private void InitializeMapGenerator()
        {
            mapGenerator = new MapGenerator(game, spriteBatch, screenBounds);
        }

        private void PrepareNewGame()
        {
            entitiesManager.Clear();
            physicsManager.Clear();
            managerUI.Clear();

            entitiesManager.AddEntity(ball);
            entitiesManager.AddEntity(paddle);

            physicsManager.AddPhysicsEntity(ball);
            physicsManager.AddPhysicsEntity(paddle);

            managerUI.AddEntity(hp);

            paddle.Transform.Position.X = screenCenter.X;
            ball.SetOnPaddle(true);
            paddle.SetBall(ball);

            hp.SetHeart(INIT_LIFE_COUNT);

            GenerateMap();

            gameOver = false;
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
