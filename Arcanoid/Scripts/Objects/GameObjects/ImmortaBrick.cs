using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid {

    public class ImmortalBrick : Brick
    {

        public ImmortalBrick(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D sprite, int lifeCount = 1) : base(spriteBatch, startPosition, sprite)
        {
            //DUMMY
        }

        #region Physics

        public override void OnCollision(IPhysicsBody collider)
        {
            //DUMMY
        }

        #endregion

    }
}