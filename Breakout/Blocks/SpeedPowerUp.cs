using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class SpeedPowerUp : StandardBlock {
        public SpeedPowerUp(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 1;
            Value = 1;
            Type = "SpeedPowerUp";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override string DecreaseHitpoints() {
            return "SpeedPowerUp";
        }
    }
}