using DIKUArcade.Math;

namespace Breakout.Blocks {
    public class Invincible : Block {
        public Invincible(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "Invincible";
        }
    }
}