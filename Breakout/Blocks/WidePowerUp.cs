using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class WidePowerUp : Block {
        public WidePowerUp(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "WidePowerUp";
        }
    }
}