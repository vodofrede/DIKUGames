using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Power Up block which makes the paddle faster
    /// </summary
    public class SpeedPowerUp : Block {
        public SpeedPowerUp(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "SpeedPowerUp";
        }
    }
}