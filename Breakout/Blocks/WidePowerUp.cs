using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Power Up block that makes the paddle wider.
    /// </summary
    public class WidePowerUp : Block {
        public WidePowerUp(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "WidePowerUp";
        }
    }
}