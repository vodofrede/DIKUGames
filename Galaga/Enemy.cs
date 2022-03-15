using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Enemy : Entity {
        private int hitpoints = 10;
        private bool enraged = false;

        private IBaseImage altImage;

        public int Hitpoints {
            get { return hitpoints; }
        }

        public bool Enraged {
            get { return enraged; }
        }

        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage altImage)
            : base(shape, image) {
                this.altImage = altImage;
            }

        public void SetEnragedToTrue() {
            enraged = true;
            Image = altImage;
        }

        public void DecreaseHitpoints() {
            hitpoints--;
        }
    }
}
