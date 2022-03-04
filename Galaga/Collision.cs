using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Galaga;

public class Collision {
    public static bool Between(Entity entity1, Entity entity2) {
        var shape1 = entity1.Shape;
        var shape2 = entity2.Shape;

        Vec2F shape1LowerLeft = new Vec2F(shape1.Position.X, shape1.Position.Y);
        Vec2F shape1UpperRight = new Vec2F(shape1.Position.X + shape1.Extent.X,
            shape1.Position.Y + shape1.Extent.Y);

        Vec2F shape2LowerLeft = new Vec2F(shape2.Position.X, shape2.Position.Y);
        Vec2F shape2UpperRight = new Vec2F(shape2.Position.X + shape2.Extent.X,
            shape2.Position.Y + shape2.Extent.Y);

        bool insideXRight = shape1UpperRight.X > shape2LowerLeft.X && shape1UpperRight.X < shape2UpperRight.X;
        bool insideYRight = shape1UpperRight.Y > shape2LowerLeft.Y && shape1UpperRight.Y < shape2UpperRight.Y;
        bool insideXLeft = shape1LowerLeft.X < shape2UpperRight.X && shape1LowerLeft.X > shape2LowerLeft.X;
        bool insideYLeft = shape1LowerLeft.Y < shape2UpperRight.Y && shape1LowerLeft.Y > shape2LowerLeft.Y;
        
        return insideXRight && insideYRight || insideXLeft && insideYLeft;
    }
}