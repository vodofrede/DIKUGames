using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class DoubleSize : Block {
        public DoubleSize(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "DoubleSize";
        }
    }
}