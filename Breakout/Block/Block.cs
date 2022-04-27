using DIKUArcade.Math;

namespace Breakout.Block {
    public class Block : IBlock {
        private int hitpoints = 1;
        private Vec2F position;

        public Block(Vec2F position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            if (hitpoints > 0) {
                hitpoints--;
            }
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