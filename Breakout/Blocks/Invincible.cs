using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class Invincible : StandardBlock {
        public Invincible(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 1;
            Value = 1;
            Type = "Invincible";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override string DecreaseHitpoints() {
            return "Invincible";
        }
    }
}