using Arkanoid.GameObjects;
using Arkanoid.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Arkanoid.Scenes
{

    /// <summary>
    /// Main game scene
    /// </summary>
    public class MainGame : Scene
    {
        private Vector2 textSpace = new Vector2(0, 5);

        private const string GAME_OVER_TEXT = "Game Over";
        private const string RESTART_TEXT = "Press ENTER to restart";
        private const int INIT_LIFE_COUNT = 3;

        MapGenerator mapGenerator;

        private DrawableEntity background;
        private Ball ball;
        private Paddle paddle;

        private HeartHP hp;
        private DrawableEntity gameOverForeground;
        private TextLabel gameOverText;
        private TextLabel restartText;

        private bool gameOver;

        public MainGame(GameController game) : base(game)
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
            if (hp.GetLifeCount() > 0 && ball.Transform.Position.Y == game.ScreenBounds.Bottom - ball.SpriteRenderer.GetHeight() / 2)
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
            base.Draw(gameTime);
        }

        #endregion

        #region Initialize

        public override void Initialize()
        {
            LoadTextures();
            LoadFonts();

            InitializeBackground();
            InitializePaddle();
            InitializeBall();
            InitializeMapGenerator();

            InitializeUI();

            PrepareNewGame();

            entities = entitiesManager.GetEntities();
        }

        private void LoadTextures()
        {
            game.Content.Load<Texture2D>(GameResources.BACKGROUND_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.GAMEOVER_FOREGROUND_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.HEART_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BALL_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.PADDLE_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BRICK_RED_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BRICK_YELLOW_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BRICK_PURPLE_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BRICK_BLUE_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BRICK_GREEN_TEXTURE);
            game.Content.Load<Texture2D>(GameResources.BRICK_GREY_TEXTURE);
        }

        private void LoadFonts()
        {
            game.Content.Load<SpriteFont>(GameResources.GAMEOVER_FONT);
            game.Content.Load<SpriteFont>(GameResources.RESTART_FONT);
        }

        private void InitializeBackground()
        {
            Texture2D backgroundTexture = game.Content.Load<Texture2D>(GameResources.BACKGROUND_TEXTURE);
            background = new DrawableEntity(backgroundTexture, spriteBatch, game.ScreenCenter);
        }

        private void InitializeBall()
        {
            Texture2D ballTexture = game.Content.Load<Texture2D>(GameResources.BALL_TEXTURE);
            Vector2 position = game.ScreenCenter;
            ball = new Ball(spriteBatch, position, ballTexture, paddle);
            ball.Transform.Scale.X = 1f;
            ball.Transform.Scale.Y = 1f;
            ball.SetBounds(game.ScreenBounds);
        }

        private void InitializePaddle()
        {
            float scaleX = 1f;
            float scaleY = 1f;
            Texture2D paddleTexture = game.Content.Load<Texture2D>(GameResources.PADDLE_TEXTURE);
            Vector2 position = new Vector2(game.ScreenBounds.Right / 2f, game.ScreenBounds.Bottom-paddleTexture.Height/2 * scaleY);
            paddle = new Paddle(spriteBatch, position, paddleTexture);
            paddle.Transform.Scale.X = scaleX;
            paddle.Transform.Scale.Y = scaleY;
            paddle.SetBounds(game.ScreenBounds);
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

            Texture2D heartTexture = game.Content.Load<Texture2D>(GameResources.HEART_TEXTURE);
            Vector2 position = new Vector2(game.ScreenBounds.Left + heartTexture.Width/2f*scaleX + offsetX, game.ScreenBounds.Top + heartTexture.Height/2f * scaleY + offsetY);
            hp = new HeartHP(heartTexture, spriteBatch, 0, position);
            hp.Transform.Scale.X = scaleX;
            hp.Transform.Scale.Y = scaleY;
            managerUI.AddEntity(hp);
        }

        private void InitializeTexts()
        {
            SpriteFont gameOverFont = game.Content.Load<SpriteFont>(GameResources.GAMEOVER_FONT);
            SpriteFont restartFont = game.Content.Load<SpriteFont>(GameResources.RESTART_FONT);

            gameOverText = new TextLabel(GAME_OVER_TEXT, gameOverFont, spriteBatch, game.ScreenCenter - textSpace/2);

            Vector2 restartTextPosition = game.ScreenCenter + new Vector2(0, gameOverText.GetTextSize().Y) + textSpace/2;
            restartText = new TextLabel(RESTART_TEXT, restartFont, spriteBatch, restartTextPosition);
        }

        private void InitializeGameOverForeground()
        {
            Texture2D foregroundTexture = game.Content.Load<Texture2D>(GameResources.GAMEOVER_FOREGROUND_TEXTURE);
            gameOverForeground = new DrawableEntity(foregroundTexture, spriteBatch, game.ScreenCenter);
        }

        private void InitializeMapGenerator()
        {
            mapGenerator = new MapGenerator(game, spriteBatch, game.ScreenBounds);
        }

        private void PrepareNewGame()
        {
            entitiesManager.Clear();
            physicsManager.Clear();
            managerUI.Clear();

            entitiesManager.AddEntity(background);
            entitiesManager.AddEntity(paddle);
            entitiesManager.AddEntity(ball);

            physicsManager.AddPhysicsEntity(paddle);
            physicsManager.AddPhysicsEntity(ball);

            managerUI.AddEntity(hp);

            paddle.Transform.Position.X = game.ScreenCenter.X;
            paddle.SetBall(ball);
            ball.SetOnPaddle(true);

            hp.SetHeart(INIT_LIFE_COUNT);

            GenerateMap();

            gameOver = false;
        }

        private void GenerateMap()
        {
            List<Brick> map = mapGenerator.GenerateMapWithBlocks(7, 5, 5f, 5f);

            entitiesManager.AddEntity(map);
            physicsManager.AddPhysicsEntity(map);
        }

        #endregion
    }
}
