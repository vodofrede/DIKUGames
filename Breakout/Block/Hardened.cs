using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout.Block {
    public class Hardened : Block {
        public Hardened(Vec2F pos, string imageName) : base(pos, imageName) {
            string altImageName = imageName.Insert(imageName.Length - 3, "-damaged");
            Console.WriteLine("alt Image Name: " + altImageName);
            altImage = new Image(Path.Combine("Assets", "Images", altImageName));

            Hitpoints = 2;
            Value = 2;
            Type = "Hardened";
        }

        public override string DecreaseHitpoints() {
            Hitpoints = Math.Max(Hitpoints - 1, 0);
            return NoMoreHitpoints() ? "Destroy" : "None";
        }
    }
}