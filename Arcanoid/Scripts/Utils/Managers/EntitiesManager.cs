using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arkanoid.Managers
{
    /// <summary>
    /// Class for managing entities and IDrawable entities in the scene
    /// </summary>
    public class EntitiesManager : IDrawable, IUpdateable
    {
        private List<Entity> entities;
        private List<IDrawable> drawableEntities;

        public EntitiesManager()
        {
            entities = new List<Entity>();
            drawableEntities = new List<IDrawable>();
        }

        /// <summary>
        /// Adds entity to be invoked by manager
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(Entity entity)
        {
            entities.Add(entity);

            if (entity is IDrawable)
                drawableEntities.Add((IDrawable)entity);
        }

        /// <summary>
        /// Adds list of entities to be invoked by manager
        /// </summary>
        /// <typeparam name="T">class inheriting from Entity</typeparam>
        /// <param name="entitiesList"></param>
        public void AddEntity<T>(List<T> entitiesList) where T : Entity
        {
            for(int i=0; i<entitiesList.Count; i++)
                AddEntity(entitiesList[i]);
        }


        /// <summary>
        /// Removes entity from manager
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);

            if (entity is IDrawable)
                drawableEntities.Remove((IDrawable)entity);
        }

        /// <summary>
        /// Returns all entities in the manager
        /// </summary>
        /// <returns></returns>
        public List<Entity> GetEntities()
        {
            return entities;
        }

        /// <summary>
        /// Removes every entity from manager
        /// </summary>
        public void Clear()
        {
            entities.Clear();
            drawableEntities.Clear();
        }


        #region Update

        /// <summary>
        /// Invokes update method in every entity in the manager
        /// </summary>
        /// <param name="gameTime">object containing game time passed from class Game</param>
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Update(gameTime);
            }
        }

        #endregion

        #region Draw
        /// <summary>
        /// Invokes draw method in every IDrawable entity in the manager
        /// </summary>
        /// <param name="gameTime">object containing game time passed from class Game</param>
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < drawableEntities.Count; i++)
                drawableEntities[i].Draw(gameTime);
        }

    #endregion

    }
}
