using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
    public class GameController : IGameComponent
    {
        EntitiesManager entitiesManager;
        PhysicsManager physicsManager;

        private List<Entity> entities;
        private Rectangle screenBounds;
        private Game game;

        SpriteBatch spriteBatch;

        public GameController(Game game)
        {
            entitiesManager = new EntitiesManager();
            physicsManager = new PhysicsManager();
            this.game = game;
        }

        public void Initialize()
        {
            InitializeScreenBounds();

            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            InitializeBall();
            InitializeMap();
            InitializePaddle();
            entities = entitiesManager.GetEntities();
        }

        public void Update(GameTime gameTime)
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

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            entitiesManager.Draw(gameTime);
            spriteBatch.End();

        }

        public void DestroyEntity(Entity entity)
        {
            entitiesManager.RemoveEntity(entity);

            if (entity is IPhysicsEntity)
                physicsManager.RemovePhysicsEntity((IPhysicsEntity)entity);
        }

        #region Initialize

        private void InitializeScreenBounds()
        {
            screenBounds = game.GraphicsDevice.PresentationParameters.Bounds;
        }

        private void InitializeBall()
        {
            Texture2D ballTexture = game.Content.Load<Texture2D>("ball");
            Vector2 position = new Vector2(screenBounds.Right / 2f - ballTexture.Width, screenBounds.Bottom - ballTexture.Height);
            Ball ball = new Ball(spriteBatch, position, ballTexture);
            ball.Transform.scale.X = 0.5f;
            ball.Transform.scale.Y = 0.5f;
            ball.SetBounds(screenBounds);
            entitiesManager.AddEntity(ball);
            physicsManager.AddPhysicsEntity(ball);
        }

        private void InitializePaddle()
        {
            float scaleX = 0.75f;
            float scaleY = 0.5f;
            Texture2D paddleTexture = game.Content.Load<Texture2D>("paddle");
            Vector2 position = new Vector2(screenBounds.Right / 2f, screenBounds.Bottom-paddleTexture.Height*scaleY);
            Paddle paddle = new Paddle(spriteBatch, position, paddleTexture);
            paddle.Transform.scale.X = scaleX;
            paddle.Transform.scale.Y = scaleY;
            paddle.SetBounds(screenBounds);
            entitiesManager.AddEntity(paddle);
            physicsManager.AddPhysicsEntity(paddle);
        }

        private void InitializeMap()
        {
            float scaleX = 0.75f;
            float scaleY = 0.5f;

            Texture2D brickTexture = game.Content.Load<Texture2D>("brick");
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Brick brick = new Brick(spriteBatch, new Vector2((i+0.5f) * brickTexture.Width, j * brickTexture.Height), brickTexture);
                    brick.Transform.scale.X = scaleX;
                    brick.Transform.scale.Y = scaleY;
                    entitiesManager.AddEntity(brick);
                    physicsManager.AddPhysicsEntity(brick);
                }
            }

        }

        #endregion
    }
}
