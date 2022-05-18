using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class Unbreakable : StandardBlock {
        public Unbreakable(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 1;
            Value = 1;
            Type = "Unbreakable";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override string DecreaseHitpoints() {
            return "None";
        }
    }
}