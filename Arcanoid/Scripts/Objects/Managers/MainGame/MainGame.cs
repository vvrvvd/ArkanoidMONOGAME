using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
    public class MainGame : Scene
    {
        EntitiesManager entitiesManager;
        PhysicsManager physicsManager;
        MapGenerator mapGenerator;

        private List<Entity> entities;
        private Rectangle screenBounds;

        public MainGame(Game game) : base(game)
        {
            entitiesManager = new EntitiesManager();
            physicsManager = new PhysicsManager();
        }

        public void DestroyEntity(Entity entity)
        {
            entitiesManager.RemoveEntity(entity);

            if (entity is IPhysicsBody)
                physicsManager.RemovePhysicsEntity((IPhysicsBody)entity);
        }

        #region Update

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].IsDestroyed())
                {
                    DestroyEntity(entities[i]);
                    i--;
                }
            }

            entitiesManager.Update(gameTime);
            physicsManager.Update(gameTime);

        }

        #endregion

        #region Draw
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            entitiesManager.Draw(gameTime);
            spriteBatch.End();
        }

        #endregion

        #region Initialize

        public override void Initialize()
        {
            LoadTextures();

            InitializeScreenBounds();

            InitializeBall();
            InitializePaddle();

            InitializeMapGenerator();
            GenerateMap();

            entities = entitiesManager.GetEntities();
        }

        private void LoadTextures()
        {
            game.Content.Load<Texture2D>("ballGrey");
            game.Content.Load<Texture2D>("paddleBlu");
            game.Content.Load<Texture2D>("element_yellow_rectangle");
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
            Ball ball = new Ball(spriteBatch, position, ballTexture);
            ball.Transform.scale.X = 1f;
            ball.Transform.scale.Y = 1f;
            ball.SetBounds(screenBounds);
            entitiesManager.AddEntity(ball);
            physicsManager.AddPhysicsEntity(ball);
        }

        private void InitializePaddle()
        {
            float scaleX = 1f;
            float scaleY = 1f;
            Texture2D paddleTexture = game.Content.Load<Texture2D>("paddleBlu");
            Vector2 position = new Vector2(screenBounds.Right / 2f, screenBounds.Bottom-paddleTexture.Height/2 * scaleY);
            Paddle paddle = new Paddle(spriteBatch, position, paddleTexture);
            paddle.Transform.scale.X = scaleX;
            paddle.Transform.scale.Y = scaleY;
            paddle.SetBounds(screenBounds);
            entitiesManager.AddEntity(paddle);
            physicsManager.AddPhysicsEntity(paddle);
        }

        private void InitializeMapGenerator()
        {
            mapGenerator = new MapGenerator(game, spriteBatch, screenBounds);
        }

        private void GenerateMap()
        {
            List<Brick> map = mapGenerator.GenerateMap(10, 5, 5f, 5f);

            entitiesManager.AddEntity(map);
            physicsManager.AddPhysicsEntity(map);
        }

        #endregion
    }
}
