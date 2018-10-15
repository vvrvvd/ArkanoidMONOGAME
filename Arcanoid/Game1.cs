using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid {

    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle screenBounds;

        Ball ball;
        Paddle paddle;
        EntitiesManager entitiesManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            InitializeScreenBounds();
            entitiesManager = new EntitiesManager();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            InitializeBall();
            InitializePaddle();
            InitializeMap();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            entitiesManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            entitiesManager.Draw();
            spriteBatch.End();

            base.Draw(gameTime);
        }


        #region Initialize

        private void InitializeScreenBounds()
        {
            screenBounds = GraphicsDevice.PresentationParameters.Bounds;
        }

        private void InitializeBall()
        {
            Texture2D ballTexture = Content.Load<Texture2D>("ball");
            ball = new Ball(spriteBatch, new Vector2(screenBounds.Right / 2f - ballTexture.Width , screenBounds.Bottom - ballTexture.Height), ballTexture);
            ball.transform.scale.X = 0.5f;
            ball.transform.scale.Y = 0.5f;
            ball.SetBounds(screenBounds);
            entitiesManager.AddEntity(ball);
        }

        private void InitializePaddle()
        {
            float scaleX = 0.75f;
            float scaleY = 0.5f;
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");
            paddle = new Paddle(spriteBatch, new Vector2(screenBounds.Right/2f - paddleTexture.Width * scaleX / 2f, screenBounds.Bottom - paddleTexture.Height * scaleY), paddleTexture);
            paddle.transform.scale.X = scaleX;
            paddle.transform.scale.Y = scaleY;
            paddle.SetBounds(screenBounds);
            entitiesManager.AddEntity(paddle);
        }

        private void InitializeMap()
        {
            float scaleX = 0.75f;
            float scaleY = 0.5f;
            
            Texture2D brickTexture = Content.Load<Texture2D>("brick");
            for (int i = 0; i<6; i++)
            {
                for(int j = 0; j<3; j++)
                {
                    Brick brick = new Brick(spriteBatch, new Vector2(i* brickTexture.Width + brickTexture.Width * scaleX / 3f, j* brickTexture.Height + brickTexture.Height * scaleY), brickTexture);
                    brick.transform.scale.X = scaleX;
                    brick.transform.scale.Y = scaleY;
                    entitiesManager.AddEntity(brick);
                }
            }
                
        }

        #endregion
    }
}
