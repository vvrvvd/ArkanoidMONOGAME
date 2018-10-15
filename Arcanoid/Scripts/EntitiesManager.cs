using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arcanoid
{
    public class EntitiesManager
    {
        private List<Entity> entities;
        private List<Action> collisions;

        public EntitiesManager()
        {
            entities = new List<Entity>();
            collisions = new List<Action>();
        }

        public void Update(GameTime gameTime)
        {
            CheckCollisions();

            for (int i = 0; i < entities.Count; i++)
            {
                if(entities[i].IsDestroyed())
                {
                    RemoveEntity(entities[i]);
                    i--;
                } else
                {
                    entities[i].Update(gameTime);
                }
            }
        }

        public void Draw()
        {
            for (int i = 0; i < entities.Count; i++)
                entities[i].Draw();
        }

        public void CheckCollisions()
        {

            for (int i = 0; i < entities.Count; i++)
            {
                for(int j = 0; j < entities.Count; j++)
                {
                    if (!entities[i].Tag.Equals(entities[j].Tag) && entities[i].GetRectangle().Intersects(entities[j].GetRectangle()))
                    {
                        Entity entity = entities[i];
                        Entity entity2 = entities[j];
                        collisions.Add(() => entity.OnCollision(entity2));
                    }
                }
            }

            for(int i=0; i < collisions.Count; i++)
            {
                collisions[i].Invoke();
            }

            collisions.Clear();
        }


        public void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }


    }
}
