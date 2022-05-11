using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Block {
    public class Block : Entity {
        protected const float HORIZONTALBLOCKS = 12f;
        protected const float VERTICALBLOCKS = 25f;

        public int Hitpoints { get; protected set; }
        public int Value { get; protected set; }
        public string Type { get; protected set; }

        // special block fields that may be null
        protected IBaseImage? altImage;

        public Block(Vec2F pos, string imageName) : base(new StationaryShape(new Vec2F(pos.X / HORIZONTALBLOCKS, 1f - pos.Y / VERTICALBLOCKS), new Vec2F(1f / 12f, 1f / 25f)), new Image(Path.Combine("Assets", "Images", imageName))) {
            Hitpoints = 1;
            Value = 1;
            Type = "Block";
        }

        public virtual string DecreaseHitpoints() {
            Hitpoints = Math.Max(Hitpoints - 1, 0);
            return NoMoreHitpoints() ? "Destroy" : "None";
        }

        public virtual void Update() {
            
        }

        protected virtual bool NoMoreHitpoints() {
            return Hitpoints <= 0;
        }

        public void SwapImage() {
            if (altImage != null) {
                Image = altImage;
            }
        }
    }
}