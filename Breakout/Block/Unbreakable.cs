using DIKUArcade.Math;

namespace Breakout.Block {
    public class Unbreakable : IBlock {
        private int hitpoints = 2;
        private Vec2I position;

        public Unbreakable(Vec2I position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            return BlockEffect.None;
        }

        public BlockType GetBlockType() {
            return BlockType.Unbreakable;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public Vec2I GetPosition() {
            return position;
        }
    }
}