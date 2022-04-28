using DIKUArcade.Math;

namespace Breakout.Block {
    public interface IBlock {
        public int GetHitpoints();
        public BlockEffect DecreaseHitpoints();
        public BlockType GetBlockType();
        public Vec2I GetPosition();
    }

    public enum BlockEffect {
        None,
        PowerUp
    }

    public enum BlockType {
        Hardened,
        PowerUp,
        Unbreakable,
        Standard,
    }
}