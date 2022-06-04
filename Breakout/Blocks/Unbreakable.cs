using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Block that cannot be destroyed
    /// </summary
    public class Unbreakable : Block {
        public Unbreakable(Vec2F pos, string imageName) : base(pos, imageName) { }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override BlockAction OnHit() {
            return BlockAction.None;
        }

        public override bool IsBreakable() {
            return false;
        }
    }
}