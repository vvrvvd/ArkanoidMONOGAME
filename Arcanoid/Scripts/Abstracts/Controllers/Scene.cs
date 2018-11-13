using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arkanoid
{
    public abstract class Scene : IUpdateable, IDrawable
    {
        protected Game game;
        protected SpriteBatch spriteBatch;

        public Scene(Game game)
        {
            this.game = game;
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public virtual void Update(GameTime gameTime)
        {
            //Dummy
        }

        public virtual void Draw(GameTime gameTime)
        {
            //Dummy
        }

        public virtual void Initialize()
        {
            //Dummy
        }
    }

}

