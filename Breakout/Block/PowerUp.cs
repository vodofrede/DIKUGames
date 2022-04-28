using DIKUArcade.Math;

namespace Breakout.Block {
    public class PowerUp : IBlock {
        private int hitpoints = 1;
        private Vec2I position;

        public PowerUp(Vec2I position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            if (hitpoints > 0) {
                hitpoints--;
            }
            return hitpoints == 0 ? BlockEffect.PowerUp : BlockEffect.None;
        }

        public BlockType GetBlockType() {
            return BlockType.PowerUp;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public Vec2I GetPosition() {
            return position;
        }
    }
}