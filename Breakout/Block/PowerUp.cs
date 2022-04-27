using DIKUArcade.Math;

namespace Breakout.Block {
    public class PowerUp : IBlock {
        private int hitpoints = 1;
        private Vec2F position;

        public PowerUp(Vec2F position) {
            this.position = position;
        }

        public BlockEffect DecreaseHitpoints() {
            if (hitpoints > 0) {
                hitpoints--;
            }
            return hitpoints == 0 ? BlockEffect.PowerUp : BlockEffect.None;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public Vec2F GetPosition() {
            return position;
        }
    }
}