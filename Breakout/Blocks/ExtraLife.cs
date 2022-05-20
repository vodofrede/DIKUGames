using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class ExtraLife : Block {
        public ExtraLife(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "ExtraLife";
        }
    }
}