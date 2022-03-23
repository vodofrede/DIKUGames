using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class PlayerShot : Entity {
        private static readonly Vec2F extent = new(0.008f, 0.021f);
        private static readonly Vec2F direction = new(0.0f, 0.02f);

        public PlayerShot(Vec2F position, IBaseImage image) : base(new DynamicShape(position, extent), image) {

        }

        public void UpdatePosition() {
            Shape.Position += direction;
        }
    }
}