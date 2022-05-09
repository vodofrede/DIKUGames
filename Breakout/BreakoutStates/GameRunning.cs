using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;

namespace Breakout {
    public class GameRunning : IGameState {
        private static GameRunning? instance;

        // constants
        private const int LIVES = 3;
        private const float SPEEDINCREASE = 0.003f;
        private const float MAXIMUM_ANGLE = (5 * MathF.PI) / 12;


        private float BALLSPEED = 0.01f;

        // state
        private Score score;
        private int lives = LIVES;
        private float speedIncrease = SPEEDINCREASE;

        // contained entities
        private Player player;
        private Map? map;
        private Ball ball;

        // resources
        private Text endGameText;
        private Text scoreText;
        private Text livesText;

        private GameEventBus eventBus = BreakoutBus.GetBus();
        private FileLoader fileLoader = FileLoader.GetInstance();

        public GameRunning() {
            // game entities
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            ball = new Ball(new Vec2F(0.5f, 0.05f));
            map = fileLoader.NextMap();
            score = new Score(new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));

            // text
            endGameText = new Text(string.Format("Game Over!"), new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f));
            endGameText.SetColor(new Vec3I(0, 128, 255));

            // winGameText
        }

        public static IGameState GetInstance() {
            return instance ??= new GameRunning();
        }

        public void RenderState() {
            score.RenderScore();
            if (lives == 0) {
                endGameText.RenderText();
            } else {
                player.RenderEntity();
                map?.RenderMap();
                ball.RenderEntity();
            }
        }

        public void ResetState() {
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            ball = new Ball(new Vec2F(0.5f, 0.05f));
            map = fileLoader.NextMap();
            score = new Score(new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));
        }

        public void UpdateState() {
            if (lives > 0 && map == null) {
                // entire game is won
                fileLoader.ResetMaps();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    From = this,
                    Message = "MAIN_MENU"
                });
            } else if (lives == 0) {
                // level is lost
                // restart level?
                fileLoader.ResetMaps();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    From = this,
                    Message = "MAIN_MENU"
                });
            } else {
                // game is running
                map.GetBlocks().Iterate(block => block.Update());
                player.Move();
                if (ball.Move()) {
                    ball = new Ball(new Vec2F(0.5f, 0.05f));
                }

                DynamicShape dynamicBall = ball.Shape.AsDynamicShape();
                dynamicBall.Direction.X = ball.Velocity.X;
                dynamicBall.Direction.Y = ball.Velocity.Y;

                var playerCollision = CollisionDetection.Aabb(dynamicBall, player.Shape);
                if (playerCollision.Collision) {

                    // finding middle of player shape
                    float playerMiddleX = player.Shape.Position.X + player.Shape.Extent.X / 2f;
                    // subtracting balls position to find relative impact (where on the player it hits)
                    // this value is -+ extent / 2 (in our case from -0.1f to 0.1f)
                    float relativeImpactPosition = playerMiddleX - dynamicBall.Position.X;
                    // normalizing to get value from -1 to 1
                    float normalizedRelativeImpact = relativeImpactPosition * 10;
                    // calculating angle based on relative impact and max angle
                    float bounceAngle = normalizedRelativeImpact * MAXIMUM_ANGLE;

                    // setting new x,y velocity
                    ball.Velocity.Y = BALLSPEED * MathF.Cos(bounceAngle);
                    ball.Velocity.X = BALLSPEED * -MathF.Sin(bounceAngle);

                }

                map.GetBlocks().Iterate(block => {
                    var blockCollision = CollisionDetection.Aabb(dynamicBall, block.Shape);

                    if (blockCollision.Collision) {
                        if (score.Points != 0 && score.Points % 10 == 0) {
                            BALLSPEED += speedIncrease;
                        }

                        var effect = block.DecreaseHitpoints();
                        switch (effect) {
                            case BlockEffect.Destroy:
                                block.DeleteEntity();
                                score.AddPoints(block.Value);
                                break;
                            case BlockEffect.Hungry:
                                ball = new Ball(new Vec2F(0.5f, 0.05f));
                                block.DeleteEntity();
                                score.AddPoints(block.Value);
                                break;
                            case BlockEffect.Reveal:
                                map.GetBlocks().Iterate(block => {
                                    if (block.Type == BlockType.Invisible) {
                                        block.SwapImage();
                                    }
                                });
                                break;
                            default:
                                throw new NotImplementedException();
                        }

                        switch (blockCollision.CollisionDir) {
                            case CollisionDirection.CollisionDirDown:
                            case CollisionDirection.CollisionDirUp:
                                ball.Velocity.Y = -ball.Velocity.Y;
                                break;
                            case CollisionDirection.CollisionDirLeft:
                            case CollisionDirection.CollisionDirRight:
                                ball.Velocity.X = -ball.Velocity.X;
                                break;
                            case CollisionDirection.CollisionDirUnchecked:
                                break;
                        }
                    };
                });

                if (map.GetBlocks().CountEntities() == 0) {
                    // next level
                    map = fileLoader.NextMap();
                }
            }
        }

        // Detect Collisions between ball and blocks
        // bool collision = CollisionDetection.Aabb(ball, block.Shape).Collision;

        // if (collision) {
        // decrease Hitpoints

        // if hitpoint <= 0 --> remove block and increase player score
        // }

        // Detect Collisions between ball and player
        // change velocity of ball according to where it hits the player (position + extent)

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