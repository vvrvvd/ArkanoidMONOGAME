using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arkanoid
{
    public class EntitiesManager : IUpdateable
    {
        private List<Entity> entities;
        private List<DrawableEntity> drawableEntities;

        public EntitiesManager()
        {
            entities = new List<Entity>();
            drawableEntities = new List<DrawableEntity>();
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);

            if (entity is DrawableEntity)
                drawableEntities.Add((DrawableEntity)entity);
        }

        public void AddEntity<T>(List<T> entitiesList) where T : Entity
        {
            for(int i=0; i<entitiesList.Count; i++)
                AddEntity(entitiesList[i]);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);

            if (entity is DrawableEntity)
                drawableEntities.Remove((DrawableEntity)entity);
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }


    #region Update

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }
        }

    #endregion

    #region Draw

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < drawableEntities.Count; i++)
                drawableEntities[i].Draw(gameTime);
        }

    #endregion

    }
}
