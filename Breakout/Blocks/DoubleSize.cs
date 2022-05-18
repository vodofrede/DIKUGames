using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class DoubleSize : StandardBlock {
        public DoubleSize(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 1;
            Value = 1;
            Type = "DoubleSize";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override string DecreaseHitpoints() {
            return "DoubleSize";
        }
    }
}