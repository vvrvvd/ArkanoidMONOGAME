using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Arkanoid
{
    public abstract class Scene : IUpdateable, IDrawable
    {
        protected Game game;
        protected SpriteBatch spriteBatch;
        protected EntitiesManager uiManager;
        protected EntitiesManager entitiesManager;
        protected PhysicsManager physicsManager;
        protected List<Entity> entities;

        public Scene(Game game)
        {
            this.game = game;
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            uiManager = new EntitiesManager();
            entitiesManager = new EntitiesManager();
            physicsManager = new PhysicsManager();
        }

        #region Update

        public virtual void Update(GameTime gameTime)
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

        public void DestroyEntity(Entity entity)
        {
            entitiesManager.RemoveEntity(entity);

            if (entity is IPhysicsBody)
                physicsManager.RemovePhysicsEntity((IPhysicsBody)entity);
        }

        #endregion

        #region Draw

        public virtual void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            entitiesManager.Draw(gameTime);
            spriteBatch.End();

            spriteBatch.Begin();
            uiManager.Draw(gameTime);
            spriteBatch.End();
        }

        #endregion

        #region Initialize

        public virtual void Initialize()
        {
            //Dummy
        }

        #endregion
    }

}

