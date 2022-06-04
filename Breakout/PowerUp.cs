using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    /// <summary>
    /// PowerUp class that can be instantiated with different data.
    /// </summary
    public class PowerUp : Entity {
        private const float GRAVITY_SPEED = -0.01f;

        public Action Effect;

        public PowerUp(Vec2F position, IBaseImage image, Action effect) : base(new DynamicShape(position, new Vec2F(0.08f, 0.08f)), image) {
            Effect = effect;
        }

        public void Update() {
            Shape.Position.Y += GRAVITY_SPEED;
        }
    }
}