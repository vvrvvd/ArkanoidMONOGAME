using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Arkanoid.Components;

namespace Arkanoid
{
    /// <summary>
    /// Basic class for objects (every updateable body in the scene must inherit from it)
    /// </summary>
    public abstract class Entity : IUpdateable
    {
        public Transform Transform;
        public string Tag;

        private bool isDestroyed;

        public Entity()
        {
            this.Transform = new Transform(Vector2.Zero);
        }

        public Entity(Vector2 position)
        {
            this.Transform = new Transform(position);
        }

        /// <summary>
        /// Function invoked every frame
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            //Dummy
        }

        public bool IsDestroyed()
        {
            return isDestroyed;
        }

        /// <summary>
        /// Takes object from manager invokes list
        /// </summary>
        public void Destroy()
        {
            isDestroyed = true;
        }
    }
}