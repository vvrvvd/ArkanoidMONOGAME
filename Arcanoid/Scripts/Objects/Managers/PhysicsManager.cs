using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    public class PhysicsManager
    {
        private List<Entity> entities;
        private List<IPhysicsBody> physicsEntities;
        private List<Action> collisionsActions;

        public PhysicsManager()
        {
            physicsEntities = new List<IPhysicsBody>();
            collisionsActions = new List<Action>();
        }

        #region Update

        public void Update(GameTime gameTime)
        {

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
                        IPhysicsBody collider2 = physicsEntities[j];
                        IPhysicsBody collider1 = physicsEntities[i];
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

        #endregion

        public void AddPhysicsEntity(IPhysicsBody physicsEntity)
        {
            physicsEntities.Add(physicsEntity);
        }

        public void RemovePhysicsEntity(IPhysicsBody physicsEntity)
        {
            physicsEntities.Remove(physicsEntity);
        }


    }
}
