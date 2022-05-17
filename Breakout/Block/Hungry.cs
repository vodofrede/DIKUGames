using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Block {
    public class Hungry : StandardBlock {
        public Hungry(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 2;
            Value = 2;
            Type = "Hungry";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override string DecreaseHitpoints() {
            Hitpoints = Math.Max(Hitpoints - 1, 0);
            return NoMoreHitpoints() ? "Destroy" : "Hungry";
        }
    }
}