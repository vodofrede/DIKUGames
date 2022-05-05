using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;

namespace Breakout {
    public class GameRunning : IGameState {
        // state
        private Score score;
        private bool gameOver = false;
        private float speedIncrease = 0.0003f;

        // contained entities
        private Player player;
        private EntityContainer<Block> blocks;
        private Ball ball;

        // images & animations

        // Text
        private Text endGameText;

        GameEventBus eventBus = BreakoutBus.GetBus();

        private static GameRunning? instance;

        public GameRunning() {
            // player setup
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            // ball
            ball = new Ball(new Vec2F(0.5f, 0.05f));

            // points
            score = new Score(new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));

            // endGameText
            endGameText = new Text(string.Format("Game Over!"), new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f));
            endGameText.SetColor(new Vec3I(0, 128, 255));

            // winGameText

        }

        public static IGameState GetInstance() {
            return instance ??= new GameRunning();
        }

        public void RenderState() {
            score.RenderScore();
            if (gameOver) {
                endGameText.RenderText();
            } else {
                player.RenderEntity();
                blocks.RenderEntities();
                ball.RenderEntity();
                // RENDER PLAYER
                // RENDER BLOCKS
                // RENDER BALL
            }
        }

        public void ResetState() {

        }

        public void UpdateState() {
            player.Move();
            ball.Move();
        }

        // Detect Collisions
        // bool collision = CollisionDetection.Aabb(ball, block.Shape).Collision;

        // if (collision) {
        // decrease hitpoints

        // if hitpoint <= 0 --> remove block and increase player score
        // }

        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
            switch (keyboardAction) {
                case KeyboardAction.KeyPress:
                    KeyPress(keyboardKey);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(keyboardKey);
                    break;
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "START_MOVE_RIGHT"
                    });
                    break;
                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "START_MOVE_LEFT"
                    });
                    break;
            }
        }

        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Space:
                    // START BALL?
                    break;

                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "STOP_MOVE_RIGHT"
                    });
                    break;

                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "STOP_MOVE_LEFT"
                    });
                    break;

                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        From = this,
                        Message = "CLOSE_WINDOW"
                    });
                    break;
            }
        }

    }
}