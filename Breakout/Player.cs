using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;

namespace Breakout {
    public class Player : Entity, IGameEventProcessor {
        private float moveLeft;
        private float moveRight;
        private const float MOVEMENT_SPEED = 0.035f;

        public Player(IBaseImage image) : base(new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.2f, 0.02f)), image) { }

        /// <summary>
        /// Process a game event
        /// </summary>
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

        /// <summary>
        /// Move the player
        /// </summary>
        public void Move() {
            Shape.Move();
            Shape.Position.X = Math.Clamp(Shape.Position.X, 0.0f, 1f - Shape.Extent.X);
        }

        /// <summary>
        /// Make the player move left
        /// </summary>
        public void SetMoveLeft(bool val) {
            moveLeft = val ? -MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        /// <summary>
        /// Make the player move right
        /// </summary>
        public void SetMoveRight(bool val) {
            moveRight = val ? MOVEMENT_SPEED : 0.0f;
            UpdateDirection();
        }

        /// <summary>
        /// Update the direction of the player
        /// </summary>
        private void UpdateDirection() {
            Shape.AsDynamicShape().Direction.X = moveLeft + moveRight;
        }

        /// <summary>
        /// Get the players position
        /// </summary>
        public Vec2F GetPosition() {
            return Shape.Position;
        }
    }
}