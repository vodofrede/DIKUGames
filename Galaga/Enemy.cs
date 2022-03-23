using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Enemy : Entity {
        private readonly IBaseImage altImage;

        public Vec2F StartingPosition { get; }
        public float Speed { get; set; }
        public int Hitpoints { get; private set; } = 6;
        public bool Enraged { get; private set; }

        public Enemy(DynamicShape shape, IBaseImage image, IBaseImage altImage, float speed)
            : base(shape, image) {
            this.altImage = altImage;
            StartingPosition = new Vec2F(shape.Position.X, shape.Position.Y);

            Speed = speed;
        }

        public void SetEnragedToTrue() {
            Enraged = true;
            Speed *= 4.5f;
            Image = altImage;
        }

        public void DecreaseHitpoints() {
            Hitpoints--;
        }
    }
}
