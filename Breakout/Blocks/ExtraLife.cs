using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class ExtraLife : StandardBlock {
        public ExtraLife(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 1;
            Value = 1;
            Type = "ExtraLife";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override string DecreaseHitpoints() {
            return "ExtraLife";
        }
    }
}