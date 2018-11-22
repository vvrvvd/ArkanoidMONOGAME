using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arkanoid
{
    class MainMenu : Scene
    {
        private const string TITLE_TEXT = "Arkanoid";
        private const string PRESS_KEY_TEXT = "PRESS ENTER TO START";
        private const string AUTHOR_TEXT = "Wiktor Rott";

        private Vector2 titleOffset = new Vector2(0f, -30f);
        private Vector2 pressKeyOffset = new Vector2(0f, 20f);
        private Vector2 authorOffset = new Vector2(0f, -25f);

        public MainMenu(GameController game) : base(game)
        {

        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CheckInput();
        }

        private void CheckInput()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                game.SetState(GameController.GameState.Game);
        }

        #endregion

        #region Initialize

        public override void Initialize()
        {
            LoadTextures();
            InitializeBackground();
            InitializeTitle();
            InitializePressKeyText();
            InitializeAuthorText();

            entities = entitiesManager.GetEntities();
        }

        private void LoadTextures()
        {
            game.Content.Load<Texture2D>("menuBackground");
        }

        private void LoadFonts()
        {
            game.Content.Load<SpriteFont>("menuTitleFont");
            game.Content.Load<SpriteFont>("menuPressKeyFont");
        }

        private void InitializeBackground()
        {
            Texture2D menuTexture = game.Content.Load<Texture2D>("menuBackground");
            DrawableEntity background = new DrawableEntity(menuTexture, spriteBatch, game.ScreenCenter);
            entitiesManager.AddEntity(background);
        }

        private void InitializeTitle()
        {
            SpriteFont titleFont = game.Content.Load<SpriteFont>("menuTitleFont");
            TextLabel title = new TextLabel(TITLE_TEXT, titleFont, spriteBatch, game.ScreenCenter + titleOffset);
            managerUI.AddEntity(title);
        }

        private void InitializePressKeyText()
        {
            SpriteFont pressKeyFont = game.Content.Load<SpriteFont>("menuPressKeyFont");
            FadingInOutTextLabel pressKeyText = new FadingInOutTextLabel(PRESS_KEY_TEXT, pressKeyFont, spriteBatch, game.ScreenCenter + pressKeyOffset, 1);
            managerUI.AddEntity(pressKeyText);
        }

        private void InitializeAuthorText()
        {
            SpriteFont authorFont = game.Content.Load<SpriteFont>("menuPressKeyFont");
            Vector2 position = new Vector2(game.ScreenCenter.X, game.ScreenBounds.Bottom) + authorOffset;
            TextLabel author = new TextLabel(AUTHOR_TEXT, authorFont, spriteBatch, position);
            managerUI.AddEntity(author);
        }

        #endregion
    }
}
