using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid {

    public class MainGame : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameController gameController;
        
        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            gameController = new GameController(this);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            gameController.Initialize();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            gameController.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            gameController.Draw(gameTime);

            base.Draw(gameTime);
        }



    }
}
