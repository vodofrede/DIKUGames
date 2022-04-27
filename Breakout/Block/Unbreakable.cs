using DIKUArcade.Math;

namespace Breakout.Block {
    public class Unbreakable : IBlock {
        private int hitpoints = 2;
        private Vec2F position;

        public Unbreakable(Vec2F position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            return BlockEffect.None;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public Vec2F GetPosition() {
            return position;
        }
    }
}