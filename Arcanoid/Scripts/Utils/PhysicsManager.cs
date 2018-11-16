using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid
{
    public class PhysicsManager : IUpdateable
    {
        private List<IPhysicsBody> physicsEntities;

        public PhysicsManager()
        {
            physicsEntities = new List<IPhysicsBody>();
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
                        collider1.OnCollision(collider2);
                        collider2.OnCollision(collider1);
                    }
                }
            }
        }

        #endregion

        public void AddPhysicsEntity(IPhysicsBody physicsEntity)
        {
            physicsEntities.Add(physicsEntity);
        }

        public void AddPhysicsEntity<T>(List<T> physicsEntitiesList) where T : IPhysicsBody
        {
            for (int i = 0; i < physicsEntitiesList.Count; i++)
                AddPhysicsEntity(physicsEntitiesList[i]);
        }

        public void RemovePhysicsEntity(IPhysicsBody physicsEntity)
        {
            physicsEntities.Remove(physicsEntity);
        }

    }
}
