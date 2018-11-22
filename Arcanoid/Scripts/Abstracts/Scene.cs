using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    public abstract class Scene : IUpdateable, IDrawable
    {
        protected GameController game;
        protected SpriteBatch spriteBatch;
        protected EntitiesManager managerUI;
        protected EntitiesManager entitiesManager;
        protected PhysicsManager physicsManager;
        protected List<Entity> entities;

        public Scene(GameController game)
        {
            this.game = game;
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            managerUI = new EntitiesManager();
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
            managerUI.Update(gameTime);
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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            entitiesManager.Draw(gameTime);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            managerUI.Draw(gameTime);
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

