using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;

public class PlayerShot : Entity {
    private static Vec2F extent = new Vec2F(0.1f, 0.021f);
    private static Vec2F direction = new Vec2F(0.0f, 0.02f);

    public PlayerShot(Vec2F position, IBaseImage image) : base(new DynamicShape(position, extent), image) {

    }

    public void UpdatePosition() {
        Shape.Position += direction;
    }
}