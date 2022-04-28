using DIKUArcade.Math;

namespace Breakout.Block {
    public class Standard : IBlock {
        private int hitpoints = 1;
        private Vec2I position;

        public Standard(Vec2I position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            if (hitpoints > 0) {
                hitpoints--;
            }
            return BlockEffect.None;
        }

        public BlockType GetBlockType() {
            return BlockType.Standard;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public Vec2I GetPosition() {
            return position;
        }
    }
}