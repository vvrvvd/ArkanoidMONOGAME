using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid {

    public class GameController : Game {

        GraphicsDeviceManager graphics;

        enum GameState
        {
            Menu,
            Game
        }

        private GameState gameState;

        MainGame mainGameController;
        MainMenu mainMenuController;

        public GameController()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 380;
            Content.RootDirectory = "Content";
            gameState = GameState.Game;
        }

        protected override void Initialize()
        {
            base.Initialize();

            mainGameController = new MainGame(this);
            mainGameController.Initialize();
            mainMenuController = new MainMenu(this);
            mainMenuController.Initialize();
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

            mainGameController.Draw(gameTime);

            base.Draw(gameTime);
        }

    }
}
