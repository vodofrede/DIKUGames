using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Block {
    public class Hungry : Block {
        public Hungry(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 1;
            Value = 1;
            Type = "Unbreakable";
        }

        public override string DecreaseHitpoints() {
            return "None";
        }
    }
}