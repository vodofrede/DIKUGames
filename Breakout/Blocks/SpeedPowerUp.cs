using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class SpeedPowerUp : Block {
        public SpeedPowerUp(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "SpeedPowerUp";
        }
    }
}