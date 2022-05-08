using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class Ball : Entity {
        // associated constants
        private const float DIAMETER = 0.05f;
        private const float SPEED = 0.01f;
        private const string IMAGE = "ball.png";

        // properties
        private float radius;
        public float Radius {
            get { return radius; }
            set {
                Shape.Extent = new Vec2F(value * 2.0f, value * 2.0f);
                radius = value;
            }
        }
        public Vec2F Velocity { get; set; }
        public float Speed {
            get { return Velocity.X; }
            set { Velocity = new Vec2F(value, value); }
        }

        // constructors
        public Ball(Vec2F position) : base(new DynamicShape(position, new Vec2F(DIAMETER, DIAMETER)), new Image(Path.Combine("Assets", "Images", IMAGE))) {
            Velocity = new Vec2F(SPEED, SPEED);
        }

        public Ball(Vec2F position, IBaseImage image) : base(new DynamicShape(position, new Vec2F(DIAMETER, DIAMETER)), image) {
            Velocity = new Vec2F(SPEED, SPEED);
        }

        // methods
        public bool Move() {
            // bounce off the walls if position will be out of bounds
            float newX = Shape.Position.X + Velocity.X;
            if (!(0.0f <= newX - Radius && newX < 1.0f - Radius)) {
                Velocity.X = -Velocity.X;
            }
            float newY = Shape.Position.Y + Velocity.Y;
            if (!(newY < 1.0f - Radius)) {
                Velocity.Y = -Velocity.Y;
            }

            // TODO: bound off of paddle

            // move ball
            Shape.Position.X += Velocity.X;
            Shape.Position.Y += Velocity.Y;

            // Console.Write("X: " + Shape.Position.X.ToString("n2") + ", ");
            // Console.WriteLine("Y: " + Shape.Position.Y.ToString("n2"));

            // return a boolean of whether the top of the ball moved off the bottom of the screen
            return Shape.Position.Y + Radius <= 0.0f;
        }
    }
}