using Microsoft.Xna.Framework;


namespace Arkanoid
{
    public interface IPhysicsBody
    {
        Rectangle GetBody();
        void OnCollision(IPhysicsBody collider);
    }
}
