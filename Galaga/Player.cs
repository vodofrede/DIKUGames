using System.Dynamic;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;

public class Player : IGameEventProcessor {
    private Entity entity;
    private DynamicShape shape;
    private float moveLeft = 0.0f;
    private float moveRight = 0.0f;
    private const float MOVEMENT_SPEED = 0.01f;

    public Player(DynamicShape shape, IBaseImage image) {
        entity = new Entity(shape, image);
        this.shape = shape;
    }

    public void ProcessEvent(GameEvent gameEvent) {

        if (gameEvent.EventType == GameEventType.PlayerEvent)
        {
            switch (gameEvent.Message)
            {
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
            }
        }
    }

    public void Render() {
        entity.RenderEntity();
    }

    public void Move() {
        shape.Move();
        shape.Position.X = System.Math.Clamp(shape.Position.X, 0.0f, 1f - shape.Extent.X);
    }

    private void SetMoveLeft(bool val) {
        if (val)
        {
            moveLeft = -MOVEMENT_SPEED;
        }
        else
        {
            moveLeft = 0.0f;
        }

        UpdateDirection();
    }

    private void SetMoveRight(bool val) {
        if (val)
        {
            moveRight = MOVEMENT_SPEED;

        }
        else
        {
            moveRight = 0.0f;
        }

        UpdateDirection();
    }

    private void UpdateDirection() {
        shape.Direction.X = moveLeft + moveRight;
    }

    public Vec2F GetPosition() {
        return shape.Position;
    }


}