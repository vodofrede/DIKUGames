using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class Hungry : Block {
        public Hungry(Vec2F pos, string imageName) : base(pos, imageName) {
            Hitpoints = 2;
            Value = 2;
            Effect = "Hungry";
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override BlockAction OnHit() {
            Hitpoints = Math.Max(Hitpoints - 1, 0);
            return Hitpoints <= 0 ? BlockAction.Destroy : BlockAction.None;
        }
    }
}