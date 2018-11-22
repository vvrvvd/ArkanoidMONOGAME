using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arkanoid
{
    public class EntitiesManager : IDrawable, IUpdateable
    {
        private List<Entity> entities;
        private List<IDrawable> drawableEntities;

        public EntitiesManager()
        {
            entities = new List<Entity>();
            drawableEntities = new List<IDrawable>();
        }

        public void AddEntity(Entity entity)
        {
            entities.Add(entity);

            if (entity is IDrawable)
                drawableEntities.Add((IDrawable)entity);
        }

        public void AddEntity<T>(List<T> entitiesList) where T : Entity
        {
            for(int i=0; i<entitiesList.Count; i++)
                AddEntity(entitiesList[i]);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);

            if (entity is IDrawable)
                drawableEntities.Remove((IDrawable)entity);
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }

        public void Clear()
        {
            entities.Clear();
            drawableEntities.Clear();
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
