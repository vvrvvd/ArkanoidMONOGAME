using Microsoft.Xna.Framework;


namespace Arkanoid
{    /// <summary>
     /// Basic interface for objects which should be updated every frame
     /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// Method invoked every frame
        /// </summary>
        void Update(GameTime gameTime);
    }
}
