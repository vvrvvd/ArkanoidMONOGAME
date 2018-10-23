using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    public class PhysicsManager
    {
        private List<Entity> entities;
        private List<IPhysicsEntity> physicsEntities;
        private List<Action> collisionsActions;

        public PhysicsManager()
        {
            physicsEntities = new List<IPhysicsEntity>();
            collisionsActions = new List<Action>();
        }

        public void Update(GameTime gameTime)
        {

            for (int i = 0; i < physicsEntities.Count; i++)
            {
                if(((Entity)physicsEntities[i]).IsDestroyed())
                {
                    RemovePhysicsEntity(physicsEntities[i]);
                    i--;
                }
            }

            CheckCollisions();

        }

        public void CheckCollisions()
        {

            for (int i = 0; i < physicsEntities.Count; i++)
            {
                for (int j = 0; j < physicsEntities.Count; j++)
                {
                    if (!physicsEntities[i].Equals(physicsEntities[j]) && physicsEntities[i].GetBody().Intersects(physicsEntities[j].GetBody()))
                    {
                        IPhysicsEntity collider2 = physicsEntities[j];
                        IPhysicsEntity collider1 = physicsEntities[i];
                        collisionsActions.Add(() => collider1.OnCollision(collider2));
                    }
                }
            }

            for (int i = 0; i < collisionsActions.Count; i++)
            {
                collisionsActions[i].Invoke();
            }

            collisionsActions.Clear();
        }

        public void AddPhysicsEntity(IPhysicsEntity physicsEntity)
        {
            physicsEntities.Add(physicsEntity);
        }

        public void RemovePhysicsEntity(IPhysicsEntity physicsEntity)
        {
            physicsEntities.Remove(physicsEntity);
        }


    }
}
