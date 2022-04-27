using DIKUArcade.Math;

namespace Breakout.Block {
    public interface IBlock {
        public int GetHitpoints();
        public BlockEffect DecreaseHitpoints();
        public Vec2F GetPosition();
    }

    public enum BlockEffect {
        None,
        PowerUp
    }
}