using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Arkanoid
{
    public class ManagerUI : IDrawable
    {
        private List<IDrawable> drawables;

        public ManagerUI()
        {
            drawables = new List<IDrawable>();
        }

        public void AddEntity<T>(T entity) where T : IDrawable
        {
            drawables.Add(entity);
        }

        public void AddEntity<T>(List<T> entitiesList) where T : IDrawable
        {
            for (int i = 0; i < entitiesList.Count; i++)
                AddEntity(entitiesList[i]);
        }

        public void RemoveEntity(IDrawable entity)
        {
            drawables.Remove(entity);
        }

        public List<IDrawable> GetEntities()
        {
            return drawables;
        }

        public void Clear()
        {
            drawables.Clear();
        }

        #region Draw

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < drawables.Count; i++)
                drawables[i].Draw(gameTime);
        }

        #endregion

    }
}
