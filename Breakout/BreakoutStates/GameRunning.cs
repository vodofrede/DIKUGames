using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using DIKUArcade.Timers;

namespace Breakout {
    public class GameRunning : IGameState, IGameEventProcessor {
        private static GameRunning? instance;

        // constants
        private const int LIVES = 3;
        private const float SPEEDINCREASE = 0.003f;
        private const float MAXIMUM_ANGLE = 5 * MathF.PI / 12;
        private const float INITIAL_BALLSPEED = 0.01f;

        // state
        private Score score;
        private int lives = LIVES;
        private float speedIncrease = SPEEDINCREASE;
        private float ballSpeed = INITIAL_BALLSPEED;
        private Text livesLeftText;
        private bool invincible = false;

        // contained entities
        private long levelTimeLimit;
        private long levelStartTime;
        private long levelCurrentTime;
        private Player player;
        private GameOver gameOver;
        private Map? map;
        private Ball ball;
        private EntityContainer<PowerUp> PowerUps;

        private GameEventBus eventBus = BreakoutBus.GetBus();
        private FileLoader fileLoader = FileLoader.GetInstance();

        public GameRunning() {
            gameOver = (GameOver)GameOver.GetInstance();

            // game entities
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.TimedEvent, this);

            ball = new Ball(new Vec2F(0.5f, 0.15f));
            map = fileLoader.NextMap();
            score = new Score(new Vec2F(0.8f, 0.8f), new Vec2F(0.2f, 0.2f));

            // livesLeftText
            livesLeftText = new Text("Lives left: " + lives, new Vec2F(0.2f, 0.8f), new Vec2F(0.2f, 0.2f));
            livesLeftText.SetFontSize(1000);
            livesLeftText.SetColor(new Vec3I(0, 128, 255));

            PowerUps = new EntityContainer<PowerUp>();
            levelStartTime = StaticTimer.GetElapsedMilliseconds();
            levelTimeLimit = map.GetTimeLimit();
        }

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        public static IGameState GetInstance() {
            return instance ??= new GameRunning();
        }

        /// <summary>
        /// Render State 
        /// </summary>
        public void RenderState() {
            player.RenderEntity();
            map?.RenderMap();
            PowerUps.RenderEntities();
            ball.RenderEntity();
            score.RenderScore();
            livesLeftText.RenderText();
        }


        /// <summary>
        /// Reset State
        /// </summary>
        public void ResetState() {
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            ball = new Ball(new Vec2F(0.5f, 0.15f));
            ballSpeed = INITIAL_BALLSPEED;
            map = fileLoader.NextMap();
            score = new Score(new Vec2F(0.8f, 0.8f), new Vec2F(0.2f, 0.2f));
            lives = LIVES;
            livesLeftText.SetText("Lives left: " + lives);
        }

        /// <summary>
        /// Update State
        /// </summary>
        public void UpdateState() {
            levelCurrentTime = StaticTimer.GetElapsedMilliseconds();
            bool timeExpired = levelCurrentTime - levelStartTime > levelTimeLimit;
            Console.WriteLine("Level start time: " + levelStartTime);
            Console.WriteLine("Level time limit: " + levelTimeLimit);
            Console.WriteLine("Level current time: " + levelCurrentTime);
            Console.WriteLine("Time Expired: " + timeExpired);
            if (lives > 0 && map == null) {
                // entire game is won
                // go to game over menu
                fileLoader.ResetMaps();
                gameOver.WonGame = true;
                // gameOver.Score = score.Points;
                // WonGame = false;
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    From = this,
                    Message = "GAME_OVER"
                });
            } else if (lives == 0 || timeExpired) {
                // gameOver.Score = score.Points;
                // level is lost
                // go to game over menu
                fileLoader.ResetMaps();
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    From = this,
                    Message = "GAME_OVER"
                });
            } else {
                eventBus.ProcessEventsSequentially();

                // game is running
                map?.GetBlocks().Iterate(block => block.Update());
                PowerUps.Iterate(powerUp => {
                    powerUp.Update();
                    var dynamic = powerUp.Shape.AsDynamicShape();
                    dynamic.Direction.X = 0.0f;
                    dynamic.Direction.Y = -0.01f;
                    if (CollisionDetection.Aabb(dynamic, player.Shape).Collision) {
                        switch (powerUp.Effect) {
                            case "ExtraLife":
                                lives++;
                                powerUp.DeleteEntity();
                                break;
                            case "WidePowerUp":
                                if (player.Shape.Extent.X < 0.4f) player.Shape.ScaleXFromCenter(2f);
                                eventBus.RegisterTimedEvent(new GameEvent {
                                    EventType = GameEventType.PlayerEvent,
                                    From = this,
                                    To = this,
                                    Message = "WIDE_STOP"
                                }, TimePeriod.NewMilliseconds(5000));
                                powerUp.DeleteEntity();
                                break;
                            case "SpeedPowerUp":
                                if (player.MovementSpeed < 0.08) player.MovementSpeed = Player.MOVEMENT_SPEED * 1.5f;
                                eventBus.RegisterTimedEvent(new GameEvent {
                                    EventType = GameEventType.PlayerEvent,
                                    From = this,
                                    To = this,
                                    Message = "SPEED_STOP"
                                }, TimePeriod.NewMilliseconds(5000));
                                powerUp.DeleteEntity();
                                break;
                            case "Invincible":
                                invincible = true;
                                eventBus.RegisterTimedEvent(new GameEvent {
                                    EventType = GameEventType.PlayerEvent,
                                    From = this,
                                    To = this,
                                    Message = "INVINCIBLE_STOP"
                                }, TimePeriod.NewMilliseconds(5000));
                                powerUp.DeleteEntity();
                                break;
                            case "DoubleSize":
                                if (ball.Shape.Extent.X < 0.41f) ball.Shape.ScaleFromCenter(2f);
                                eventBus.RegisterTimedEvent(new GameEvent {
                                    EventType = GameEventType.PlayerEvent,
                                    From = this,
                                    To = this,
                                    Message = "DOUBLE_SIZE_STOP"
                                }, TimePeriod.NewMilliseconds(5000));
                                powerUp.DeleteEntity();
                                break;
                            default:
                                powerUp.DeleteEntity();
                                break;
                        }
                    }
                    if (powerUp.Shape.Position.Y < -1.0f) {
                        powerUp.DeleteEntity();
                    }
                });

                player.Move();
                if (ball.Move()) {
                    ball = new Ball(new Vec2F(0.5f, 0.5f));
                    if (!invincible) lives--;
                }

                DynamicShape dynamicBall = ball.Shape.AsDynamicShape();
                dynamicBall.Direction.X = ball.Velocity.X;
                dynamicBall.Direction.Y = ball.Velocity.Y;

                var paddleCollision = CollisionDetection.Aabb(dynamicBall, player.Shape);
                if (paddleCollision.Collision && ball.Velocity.Y < 0.0f) {
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
                    ball.Velocity.Y = ballSpeed * MathF.Cos(bounceAngle);
                    ball.Velocity.X = ballSpeed * -MathF.Sin(bounceAngle);
                }

                map?.GetBlocks().Iterate(block => {
                    var blockCollision = CollisionDetection.Aabb(dynamicBall, block.Shape);

                    if (blockCollision.Collision) {
                        if (score.Points != 0 && score.Points % 10 == 0) {
                            ballSpeed += speedIncrease;
                        }

                        Vec2F blockPosition = block.Shape.Position;
                        var effect = block.DecreaseHitpoints();
                        switch (effect) {
                            case "Destroy":
                                block.DeleteEntity();
                                score.AddPoints(block.Value);
                                break;
                            case "Hungry":
                                ball = new Ball(new Vec2F(0.5f, 0.15f));
                                block.DeleteEntity();
                                score.AddPoints(block.Value);
                                break;
                            case "ExtraLife":
                                PowerUp extraLife = new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "heart_filled.png")), "ExtraLife");
                                PowerUps.AddEntity(extraLife);
                                block.DeleteEntity();
                                break;
                            case "WidePowerUp":
                                PowerUp widePowerUp = new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")), "WidePowerUp");
                                PowerUps.AddEntity(widePowerUp);
                                block.DeleteEntity();
                                break;
                            case "DoubleSize":
                                PowerUp doubleSize = new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "BigPowerUp.png")), "DoubleSize");
                                PowerUps.AddEntity(doubleSize);
                                block.DeleteEntity();
                                break;
                            case "Invincible":
                                PowerUp invincible = new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "InfinitePowerUp.png")), "Invincible");
                                PowerUps.AddEntity(invincible);
                                block.DeleteEntity();
                                break;
                            case "SpeedPowerUp":
                                PowerUp speedPowerUp = new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png")), "SpeedPowerUp");
                                PowerUps.AddEntity(speedPowerUp);
                                block.DeleteEntity();
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

                bool anyBlocksLeft = false;
                map?.GetBlocks().Iterate(block => {
                    if (block.Type != "Unbreakable") {
                        anyBlocksLeft = true;
                    }
                });
                if (!anyBlocksLeft) {
                    // next level
                    map = fileLoader.NextMap();
                    ResetState();
                }

            }
            livesLeftText.SetText("Lives left: " + lives);
        }

        /// <summary>
        /// Handle Key Events
        /// </summary>
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

        /// <summary>
        /// Handle Key Presses
        /// </summary>
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

        /// <summary>
        /// Handle Key Releases
        /// </summary>
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

                case KeyboardKey.P:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.GameStateEvent,
                        From = this,
                        Message = "GAME_PAUSED"
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

        public void ProcessEvent(GameEvent gameEvent) {
            Console.WriteLine("Got game event");
            Console.WriteLine(gameEvent);

            switch (gameEvent.Message) {
                case "WIDE_STOP":
                    player.Shape.Extent.X = 0.2f;
                    break;
                case "INVINCIBLE_STOP":
                    invincible = false;
                    break;
                case "SPEED_STOP":
                    player.MovementSpeed = Player.MOVEMENT_SPEED;
                    break;
                case "DOUBLE_SIZE_STOP":
                    ball.Shape.Extent = new Vec2F(0.1f, 0.1f);
                    break;
            }
        }
    }
}