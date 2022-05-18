using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout.Blocks {
    public enum BlockEffect {
        None,
        Destroy
    }

    public class Block : Entity {
        protected const float HORIZONTAL_BLOCKS = 12f;
        protected const float VERTICAL_BLOCKS = 25f;

        public int Hitpoints { get; protected set; }
        public int Value { get; protected set; }

        public Block(DynamicShape shape, IBaseImage image) : base(shape, image) { }

        public BlockEffect OnHit() {
            return Hitpoints > 0 ? BlockEffect.Destroy : BlockEffect.None;
        }
    }
}