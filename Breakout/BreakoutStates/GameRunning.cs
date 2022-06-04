using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.Timers;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// Game State class for handling the Game Running state
    /// This class should be used as part of a State Machine instance.
    /// </summary
    public class GameRunning : IGameStateExt, IGameEventProcessor {
        // constants
        private const int LIVES = 3;
        private const float SPEEDINCREASE = 0.003f;
        private const float MAXIMUM_ANGLE = 3 * MathF.PI / 12;
        private const float INITIAL_BALLSPEED = 0.01f;

        // fields
        private EventBus eventBus;
        private LevelLoader levelLoader = new();

        // game variables
        private int lives = LIVES;
        private int score = 0;
        private bool invincible = false;
        private float ballSpeed = INITIAL_BALLSPEED;
        private float speedIncrease = SPEEDINCREASE;
        private Player player;
        private Level? level;
        private Ball ball;
        private EntityContainer<PowerUp> powerUps = new();
        private TextDisplay textDisplay = new();
        private int startTime = (int)Math.Floor(StaticTimer.GetElapsedSeconds());

        public GameRunning() {
            eventBus = EventBus.GetInstance();
            eventBus.Subscribe(GameEventType.TimedEvent, this);

            // player setup
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.TimedEvent, this);

            // other entities
            ball = new Ball(new Vec2F(0.5f, 0.15f));
            level = levelLoader.Next();

            // on-screen text
            textDisplay.AddTextField(new TextField(() => "Lives left: " + lives, new Vec2F(0.2f, 0.8f), new Vec2F(0.2f, 0.2f)));
            textDisplay.AddTextField(new TextField(() => "score: " + score, new Vec2F(0.8f, 0.8f), new Vec2F(0.2f, 0.2f)));
            textDisplay.AddTextField(new TextField(() => "Time left: " + TimeLeft() + "s", new Vec2F(0.8f, 0.2f), new Vec2F(0.2f, 0.2f)));
        }

        /// <summary>
        /// RenderText State 
        /// </summary>
        public void RenderState() {
            player.RenderEntity();
            level?.Render();
            ball.RenderEntity();
            powerUps.RenderEntities();
            textDisplay.RenderText();
        }

        /// <summary>
        /// Reset State
        /// </summary>
        public void ResetState() {
            levelLoader = new();
            level = levelLoader.Next();

            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            ball = new Ball(new Vec2F(0.5f, 0.15f));
            ballSpeed = INITIAL_BALLSPEED;
            speedIncrease = SPEEDINCREASE;

            powerUps = new();
            startTime = (int)Math.Floor(StaticTimer.GetElapsedSeconds());

            lives = LIVES;
        }

        /// <summary>
        /// Ingest variables from other state
        /// </summary>
        public void SetState(object extraState) { }

        private int TimeLeft() {
            var elapsed = (int)Math.Floor(StaticTimer.GetElapsedSeconds()) - startTime;
            return (level?.TimeLimit - elapsed) ?? 300;
        }

        /// <summary>
        /// Update State
        /// </summary>
        public void UpdateState() {
            eventBus.ProcessEventsSequentially();

            // process powerups
            powerUps.Iterate(powerUp => {
                powerUp.Update();
                var dynamic = powerUp.Shape.AsDynamicShape();
                dynamic.Direction.X = 0.0f;
                dynamic.Direction.Y = -0.01f;
                if (CollisionDetection.Aabb(dynamic, player.Shape).Collision) {
                    powerUp.Effect();
                    powerUp.DeleteEntity();
                }
                if (powerUp.Shape.Position.Y < -1.0f) {
                    powerUp.DeleteEntity();
                }
            });

            player.Move();
            if (ball.Move()) {
                // ball has gone off the bottom of the screen
                ball = new Ball(new Vec2F(0.5f, 0.15f));
                if (!invincible) lives--;
            }

            // check for collision between paddle and ball
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

            // check for collision between ball and blocks
            bool ballDirChanged = false;
            level?.Blocks.Iterate(block => {
                var blockCollision = CollisionDetection.Aabb(dynamicBall, block.Shape);
                if (blockCollision.Collision) {
                    // increase speed if score is multiple of 10 
                    // or score + 1, since it is possible to destroy multiple blocks at once
                    if (score != 0 && (score % 10 == 0 || score + 1 % 10 == 0)) {
                        ballSpeed += speedIncrease;
                    }

                    // apply effect
                    Vec2F blockPosition = block.Shape.Position;
                    switch (block.OnHit()) {
                        case BlockAction.None: break;
                        case BlockAction.Destroy:
                            switch (block.Effect) {
                                case "None": break;
                                case "Hungry":
                                    ball = new Ball(new Vec2F(0.5f, 0.15f));
                                    break;
                                case "ExtraLife":
                                    powerUps.AddEntity(new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "heart_filled.png")), () => lives++));
                                    break;
                                case "WidePowerUp":
                                    powerUps.AddEntity(new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "WidePowerUp.png")), () => {
                                        player.Shape.ScaleXFromCenter(player.Shape.Extent.X < 0.4f ? 2f : 1f);
                                        eventBus.AddOrResetTimedEvent(this, "WIDE_STOP", 5000);
                                    }));
                                    break;
                                case "DoubleSize":
                                    powerUps.AddEntity(new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "BigPowerUp.png")), () => {
                                        ball.Shape.ScaleFromCenter(ball.Shape.Extent.X < 0.15f ? 1.5f : 1f);
                                        eventBus.AddOrResetTimedEvent(this, "DOUBLE_SIZE_STOP", 5000);
                                    })); ;
                                    break;
                                case "Invincible":
                                    powerUps.AddEntity(new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "InfinitePowerUp.png")), () => {
                                        invincible = true;
                                        eventBus.AddOrResetTimedEvent(this, "INVINCIBLE_STOP", 20000);
                                    }));
                                    break;
                                case "SpeedPowerUp":
                                    powerUps.AddEntity(new PowerUp(blockPosition, new Image(Path.Combine("Assets", "Images", "DoubleSpeedPowerUp.png")), () => {
                                        player.MovementSpeed = player.MovementSpeed < 0.08 ? Player.MOVEMENT_SPEED * 1.5f : player.MovementSpeed;
                                        eventBus.AddOrResetTimedEvent(this, "SPEED_STOP", 5000);
                                    }));
                                    break;
                                default: throw new NotImplementedException();
                            }
                            score += block.Value;
                            block.DeleteEntity();
                            break;
                    }

                    // reflect ball
                    switch (blockCollision.CollisionDir) {
                        case CollisionDirection.CollisionDirDown:
                        case CollisionDirection.CollisionDirUp:
                            if (!ballDirChanged) {
                                ball.Velocity.Y = -ball.Velocity.Y;
                                ballDirChanged = true;
                            };
                            break;
                        case CollisionDirection.CollisionDirLeft:
                        case CollisionDirection.CollisionDirRight:
                            if (!ballDirChanged) {
                                ball.Velocity.X = -ball.Velocity.X;
                                ballDirChanged = true;
                            }
                            break;
                        case CollisionDirection.CollisionDirUnchecked:
                            break;
                    }
                };
            });

            // determine game state
            if (lives <= 0 || TimeLeft() <= 0) {
                // game has been lost
                eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SET_STATE", "GameOver", new { Score = score, Won = false });
                eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "GameOver");
            }
            if (level == null) {
                // game has been won
                eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SET_STATE", "GameOver", new { Score = score, Won = true });
                eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "GameOver");
            }
            // load next map if no blocks are left
            bool anyBlocksLeft = false;
            level?.Blocks.Iterate(block => {
                if (block.IsBreakable()) {
                    anyBlocksLeft = true;
                }
            });
            if (!anyBlocksLeft) {
                // next level, we cant reset fully as we need to keep levelLoader state.
                level = levelLoader.Next();

                player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
                eventBus.Subscribe(GameEventType.PlayerEvent, player);

                ball = new Ball(new Vec2F(0.5f, 0.15f));
                ballSpeed = INITIAL_BALLSPEED;
                speedIncrease = SPEEDINCREASE;

                powerUps = new();

                startTime = (int)Math.Floor(StaticTimer.GetElapsedSeconds());
            }
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
                    eventBus.RegisterEvent(GameEventType.PlayerEvent, this, "START_MOVE_RIGHT");
                    break;
                case KeyboardKey.Left:
                    eventBus.RegisterEvent(GameEventType.PlayerEvent, this, "START_MOVE_LEFT");
                    break;
            }
        }

        /// <summary>
        /// Handle Key Releases
        /// </summary>
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    eventBus.RegisterEvent(GameEventType.PlayerEvent, this, "STOP_MOVE_RIGHT");
                    break;
                case KeyboardKey.Left:
                    eventBus.RegisterEvent(GameEventType.PlayerEvent, this, "STOP_MOVE_LEFT");
                    break;
                case KeyboardKey.P:
                    eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "GamePaused");
                    break;
                case KeyboardKey.Escape:
                    eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW");
                    break;
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
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
                    ball.Shape.Extent = new Vec2F(0.05f, 0.05f);
                    break;
            }
        }
    }
}