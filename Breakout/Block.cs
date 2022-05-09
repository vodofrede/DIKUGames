using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public enum BlockEffect {
        None,
        Destroy,
        Hungry,
        Reveal,
        // Unlock,
        // Sequence
    }

    public enum BlockType {
        Hardened,
        Unbreakable,
        Invisible,
        Healing,
        Hungry,
        Standard,
        // Moving,
        // Sequence,
        // Teleporting,
        // Switch
    }

    public class Block : Entity {
        private const float HORIZONTALBLOCKS = 12f;
        private const float VERTICALBLOCKS = 25f;

        public int Hitpoints { get; private set; }
        public int Value { get; private set; }
        public BlockType Type { get; private set; }

        // special block fields that may be null
        private int? timer;
        private IBaseImage primaryImage;
        private IBaseImage? altImage;

        public Block(BlockType type, Vec2F pos, string imageName) : base(new StationaryShape(new Vec2F(pos.X / HORIZONTALBLOCKS, 1f - pos.Y / VERTICALBLOCKS), new Vec2F(1f / 12f, 1f / 25f)), new Image(Path.Combine("Assets", "Images", imageName))) {
            primaryImage = new Image(Path.Combine("Assets", "Images", imageName));

            switch (type) {
                case BlockType.Standard:
                case BlockType.Hungry:
                    Hitpoints = 1;
                    Value = 1;
                    break;
                case BlockType.Healing:
                    Hitpoints = 2;
                    Value = 2;
                    timer = 0;
                    break;
                case BlockType.Hardened:
                    Hitpoints = 2;
                    Value = 2;
                    string altImageName = imageName.Insert(imageName.Length - 3, "-damaged");
                    Console.WriteLine("alt Image Name: " + altImageName);
                    altImage = new Image(Path.Combine("Assets", "Images", altImageName));
                    break;
                case BlockType.Unbreakable:
                    Hitpoints = 1;
                    Value = 3;
                    break;
                case BlockType.Invisible:
                    Hitpoints = 1;
                    Value = 1;
                    altImage = primaryImage;
                    primaryImage = new Image(Path.Combine("Assets", "Levels", "empty.png")); // maybe just null will work here
                    break;
                default:
                    throw new NotImplementedException();
                    // case BlockType.Switch:
                    // case BlockType.Moving:
                    // case BlockType.Sequence:
                    // case BlockType.Teleporting:
            }
        }

        public BlockEffect DecreaseHitpoints() {
            Hitpoints = Math.Max(Hitpoints - 1, 0);

            switch (Type) {
                case BlockType.Standard:
                case BlockType.Healing:
                case BlockType.Invisible:
                    return NoMoreHitpoints() ? BlockEffect.Destroy : BlockEffect.None;
                case BlockType.Hungry:
                    return NoMoreHitpoints() ? BlockEffect.Destroy : BlockEffect.Hungry;
                case BlockType.Unbreakable:
                    return BlockEffect.None;
                case BlockType.Hardened:
                    if (Hitpoints > 0) {
                        SwapImage();
                        return BlockEffect.None;
                    } else {
                        return BlockEffect.Destroy;
                    }
                default:
                    throw new NotImplementedException();
                    // case BlockType.Moving:
                    // case BlockType.Sequence:
                    // case BlockType.Teleporting:
            }
        }

        public void Update() {
            switch (Type) {
                case BlockType.Healing:
                    timer++;
                    if (timer % 600 == 0) {
                        Hitpoints = Math.Max(Hitpoints, 2);
                    }
                    break;
                default:
                    break;
            }
        }

        public bool NoMoreHitpoints() {
            return Hitpoints <= 0;
        }

        public void SwapImage() {
            if (altImage != null) {
                Image = altImage;
            }
        }
    }
}