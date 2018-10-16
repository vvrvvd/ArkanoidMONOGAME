using Microsoft.Xna.Framework;


namespace Arkanoid
{
    public interface IPhysicsEntity
    {
        Rectangle GetBody();
        void OnCollision(Entity collider);
    }
}
