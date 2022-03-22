using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Enemy : Entity {
        private int hitpoints = 6;
        private bool enraged = false;
        private float speed;
        private Vec2F startingPosition;

        public Vec2F StartingPosition { get { return startingPosition; } }
        public float Speed { get { return speed; } set { speed = value; } }

        private IBaseImage altImage;

        public int Hitpoints {
            get { return hitpoints; }
        }

        public bool Enraged {
            get { return enraged; }
        }

        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage altImage, float speed)
            : base(shape, image) {
            this.altImage = altImage;
            this.startingPosition = shape.Position;
            this.speed = speed;
        }

        public void SetEnragedToTrue() {
            enraged = true;
            speed *= 4.5f;
            Image = altImage;
        }

        public void DecreaseHitpoints() {
            hitpoints--;
        }
    }
}
