using Microsoft.Xna.Framework;


namespace Arkanoid
{
    /// <summary>
    /// Interface for objects drawn on the screen
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Method drawing object on the screen
        /// </summary>
        /// <param name="gameTime">object containing game time passed from class Game</param>
        void Draw(GameTime gameTime);
    }
}
