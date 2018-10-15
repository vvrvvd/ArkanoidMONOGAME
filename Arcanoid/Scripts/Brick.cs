using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Arcanoid {

    public class Brick : Entity {

        public Brick(SpriteBatch spriteBatch, Vector2 startPosition, Texture2D texture) : base(spriteBatch, startPosition)
        {
            this.Texture = texture;
            Tag = "Brick";
        }

        public override void OnCollision(Entity collider)
        {
            if(collider.Tag.Equals("Ball"))
            {
                Destroy();
            }
        }

    }

}