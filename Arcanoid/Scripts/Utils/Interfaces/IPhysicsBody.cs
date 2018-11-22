using Microsoft.Xna.Framework;


namespace Arkanoid
{
    /// <summary>
    /// Interface for objects with physics on the screen
    /// </summary>
    public interface IPhysicsBody
    {
        /// <summary>
        /// Method returning collider
        /// </summary>
        Rectangle GetCollider();

        /// <summary>
        /// Trigger on collision
        /// </summary>
        /// <param name="collider">body triggering collision</param>
        void OnCollision(IPhysicsBody collider);
    }
}
