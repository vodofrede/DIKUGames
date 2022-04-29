using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout {
    public enum BlockEffect {
        None,
        PowerUp,
        Destroy
    }

    public enum BlockType {
        Hardened, 
        PowerUp,
        Unbreakable,
        Standard,
    }

    public class Block : Entity {
        private int hitpoints;
        private int value {get;}
        private BlockType type {get;}

        public Block(BlockType type, Shape shape, IBaseImage image): base(shape, image) {
            switch (type) {
                case BlockType.Standard:
                    hitpoints = 1;
                    value = 1;
                    break;
                case BlockType.Hardened:
                    hitpoints = 2;
                    value = 2;
                    break;
                case BlockType.PowerUp:
                    hitpoints = 1;
                    value = 3;
                    break;
                case BlockType.Unbreakable:
                    hitpoints = 1;
                    value = 4;
                    break;
            }
        }

        public BlockEffect DecreaseHitpoints() {
            switch (type) {
                case BlockType.Standard:
                    hitpoints = Math.Max(hitpoints - 1, 0);
                    return BlockEffect.Destroy;
                case BlockType.Hardened:
                    hitpoints = Math.Max(hitpoints - 1, 0);
                    return hitpoints == 0 ? BlockEffect.Destroy : BlockEffect.None;
                case BlockType.PowerUp:
                    hitpoints = Math.Max(hitpoints - 1, 0);
                    return BlockEffect.PowerUp;
                case BlockType.Unbreakable:
                    return BlockEffect.None;
                default:
                    return BlockEffect.None;
            }
        }

        public bool NoMoreHitpoints() {
            return hitpoints <= 0;
        }

        public int GetHitpoints() {
            return hitpoints;
        }

        public int GetValue() {
            return value;
        }
    }
}