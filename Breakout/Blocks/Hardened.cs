using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Blocks {
    /// <summary>
    /// Block which has two hitpoints and gives twice the reward
    /// </summary
    public class Hardened : Block {
        public Hardened(Vec2F pos, string imageName) : base(pos, imageName) {
            string altImageName = imageName.Insert(imageName.Length - 4, "-damaged");
            altImage = new Image(Path.Combine("Assets", "Images", altImageName));

            Hitpoints = 2;
            Value = 2;
        }

        /// <summary>
        /// Decrease hitpoints and return an effect
        /// </summary>
        public override BlockAction OnHit() {
            SwapImage();
            Hitpoints = Math.Max(Hitpoints - 1, 0);
            return Hitpoints <= 0 ? BlockAction.Destroy : BlockAction.None;
        }
    }
}