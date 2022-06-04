using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Power Up block which doubles the size of the ball
    /// </summary
    public class DoubleSize : Block {
        public DoubleSize(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "DoubleSize";
        }
    }
}