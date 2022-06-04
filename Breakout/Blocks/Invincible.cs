using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Block that gives the player invincibility (cannot take hitpoint damage).
    /// </summary
    public class Invincible : Block {
        public Invincible(Vec2F pos, string imageName) : base(pos, imageName) {
            Effect = "Invincible";
        }
    }
}