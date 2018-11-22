using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid {

    public class GameController : Game {

        public GraphicsDeviceManager Graphics;
        public Rectangle ScreenBounds;
        public Vector2 ScreenCenter;

        public enum GameState
        {
            Menu,
            Game
        }

        private GameState gameState;

        private MainGame mainGameController;
        private MainMenu mainMenuController;

        public GameController()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.PreferredBackBufferWidth = 640;
            Graphics.PreferredBackBufferHeight = 380;
            Content.RootDirectory = "Content";
            gameState = GameState.Menu;
        }

        protected override void Initialize()
        {
            base.Initialize();
            InitializeScreenBounds();

            mainGameController = new MainGame(this);
            mainGameController.Initialize();
            mainMenuController = new MainMenu(this);
            mainMenuController.Initialize();
        }

        private void InitializeScreenBounds()
        {
            ScreenBounds = GraphicsDevice.PresentationParameters.Bounds;
            ScreenCenter = new Vector2(ScreenBounds.Width / 2, ScreenBounds.Height / 2);
        }

        protected override void LoadContent()
        {
            // TODO: Load any non ContentManager content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                gameState = GameState.Menu;

            switch (gameState)
            {
                case GameState.Game:
                    mainGameController.Update(gameTime);
                    break;
                case GameState.Menu:
                    mainMenuController.Update(gameTime);
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            switch (gameState)
            {
                case GameState.Game:
                    mainGameController.Draw(gameTime);
                    break;
                case GameState.Menu:
                    mainMenuController.Draw(gameTime);
                    break;
            }
            base.Draw(gameTime);
        }

        public void SetState(GameState state)
        {
            gameState = state;
        }

        public GameState GetCurrentState()
        {
            return gameState;
        }

    }
}
