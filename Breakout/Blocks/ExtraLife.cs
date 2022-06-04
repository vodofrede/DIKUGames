using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Power Up block which gives an extra life
    /// </summary
    public class ExtraLife : Block {
        public ExtraLife(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "ExtraLife";
        }
    }
}