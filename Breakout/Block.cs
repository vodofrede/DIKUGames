using System;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
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

    public class Block : Entity {
        private int hitpoints;
        private BlockType type;

        public Block(BlockType type, Shape shape, IBaseImage image): base(shape, image) {
            switch (type) {
                case BlockType.Standard:
                    hitpoints = 1;
                    break;
                case BlockType.Hardened:
                    hitpoints = 2;
                    break;
                case BlockType.PowerUp:
                    hitpoints = 1;
                    break;
                case BlockType.Unbreakable:
                    hitpoints = 9999;
                    break;
            }
        }

        public BlockEffect DecreaseHitpoints() {
            switch (type) {
                case BlockType.Standard:
                    hitpoints = Math.Max(hitpoints - 1, 0);
                    return BlockEffect.None;
                case BlockType.Hardened:
                    hitpoints = Math.Max(hitpoints - 1, 0);
                    return BlockEffect.None;
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
    }
}