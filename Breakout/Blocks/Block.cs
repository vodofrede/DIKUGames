using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public enum BlockAction {
        None,
        Destroy
    }

    public class Block : Entity {
        protected const float HORIZONTAL_BLOCKS = 12f;
        protected const float VERTICAL_BLOCKS = 25f;

        public int Hitpoints { get; protected set; } = 1;
        public int Value { get; protected set; } = 1;
        public string Effect = "None";

        protected IBaseImage? altImage;

        public Block(Vec2F pos, string imageName) : base(new StationaryShape(new Vec2F(pos.X / HORIZONTAL_BLOCKS, 1f - pos.Y / VERTICAL_BLOCKS), new Vec2F(1f / 12f, 1f / 25f)), new Image(Path.Combine("Assets", "Images", imageName))) {

        }

        public virtual BlockAction OnHit() {
            Hitpoints = Math.Max(0, Hitpoints - 1);
            return Hitpoints <= 0 ? BlockAction.Destroy : BlockAction.None;
        }

        public virtual void Update() {

        }

        public virtual bool IsBreakable() {
            return true;
        }

        public virtual void SwapImage() {
            if (altImage != null) {
                Image = altImage;
            }
        }
    }
}