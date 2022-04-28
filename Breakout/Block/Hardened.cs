using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Block {
    public class Hardened : IBlock {
        private int hitpoints = 2;
        private Vec2I position;

        public Hardened(Vec2I position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            if (hitpoints > 0) {
                hitpoints--;
            }
            return BlockEffect.None;
        }

        public BlockType GetBlockType() {
            return BlockType.Hardened;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public Vec2I GetPosition() {
            return position;
        }
    }
}