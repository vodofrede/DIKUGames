using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class Player : IGameEventProcessor {
        private readonly Entity entity;
        private readonly DynamicShape shape;
        private float moveLeft;
        private float moveRight;
        private const float MOVEMENT_SPEED = 0.05f;

        public Player(DynamicShape shape, IBaseImage image) {
            entity = new Entity(shape, image);
            this.shape = shape;
        }

        public void ProcessEvent(GameEvent gameEvent) {

            if (gameEvent.EventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                    case "START_MOVE_RIGHT":
                        SetMoveLeft(false);
                        SetMoveRight(true);
                        break;

                    case "START_MOVE_LEFT":
                        SetMoveRight(false);
                        SetMoveLeft(true);
                        break;

                    case "STOP_MOVE_RIGHT":
                        SetMoveRight(false);
                        break;

                    case "STOP_MOVE_LEFT":
                        SetMoveLeft(false);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Render() {
            entity.RenderEntity();
        }

        public void Move() {
            shape.Move();
            shape.Position.X = Math.Clamp(shape.Position.X, 0.0f, 1f - shape.Extent.X);
        }

        private void SetMoveLeft(bool val) {
            moveLeft = val ? -MOVEMENT_SPEED : 0.0f;

            UpdateDirection();
        }

        private void SetMoveRight(bool val) {
            moveRight = val ? MOVEMENT_SPEED : 0.0f;

            UpdateDirection();
        }

        private void UpdateDirection() {
            shape.Direction.X = moveLeft + moveRight;
        }

        public Vec2F GetPosition() {
            return shape.Position;
        }


    }
}