using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Arkanoid.Managers
{
    /// <summary>
    /// Class for managing physics and collisions between IPhysicsBody objects in the scene
    /// </summary>
    public class PhysicsManager : IUpdateable
    {
        private List<IPhysicsBody> physicsEntities;

        public PhysicsManager()
        {
            physicsEntities = new List<IPhysicsBody>();
        }

        #region Update

        /// <summary>
        /// Update checking collisions of bodies in the manager
        /// </summary>
        /// <param name="gameTime">object containing game time passed from class Game</param>
        public void Update(GameTime gameTime)
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            for (int i = 0; i < physicsEntities.Count; i++)
            {
                for (int j = 0; j < physicsEntities.Count; j++)
                {
                    if (!physicsEntities[i].Equals(physicsEntities[j]) && physicsEntities[i].GetCollider().Intersects(physicsEntities[j].GetCollider()))
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

        /// <summary>
        /// Adds physics body to the manager (It doesn't have to be an entity!)
        /// </summary>
        /// <param name="physicsEntity"></param>
        public void AddPhysicsEntity(IPhysicsBody physicsEntity)
        {
            physicsEntities.Add(physicsEntity);
        }

        /// <summary>
        /// Adds list of physics body to the manager (It doesn't have to be an entities list!)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="physicsEntitiesList"></param>
        public void AddPhysicsEntity<T>(List<T> physicsEntitiesList) where T : IPhysicsBody
        {
            for (int i = 0; i < physicsEntitiesList.Count; i++)
                AddPhysicsEntity(physicsEntitiesList[i]);
        }

        /// <summary>
        /// Removes body from physics manager
        /// </summary>
        /// <param name="physicsEntity"></param>
        public void RemovePhysicsEntity(IPhysicsBody physicsEntity)
        {
            physicsEntities.Remove(physicsEntity);
        }

        /// <summary>
        /// Removes every body from manager
        /// </summary>
        public void Clear()
        {
            physicsEntities.Clear();
        }

    }
}
