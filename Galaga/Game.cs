using System; 
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using Galaga.MovementStrategy;
using Galaga.Squadron;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;

        // game state
        private Score score;
        private bool gameOver = false;
        private float speedIncrease = 0.0003f;
        private int rounds = 0;

        // contained entities
        private Player player;
        private ISquadron squadron;
        private IMovementStrategy strategy;
        private EntityContainer<PlayerShot> playerShots;

        // images & animations
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private List<Image> enemyStridesGreen;
        private List<Image> enemyStridesBlue;
        private const int EXPLOSION_LENGTH_MS = 500;

        // text
        private Text endGameText;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // event bus (only subscribed to InputEvent)
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent });

            // key input forwarding
            window.SetKeyEventHandler(KeyHandler);

            // squadron setup
            enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "GreenMonster.png"));

            squadron = new RoundSquadron();
            squadron.CreateEnemies(enemyStridesBlue, enemyStridesGreen, 0.0006f);
            
            // movement strategy setup 
            strategy = new ZigZagDown();

            // player setup
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new
                Image(Path.Combine("Assets", "Images", "Player.png"))
            );
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            // player shot setup
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            // explosion setup
            enemyExplosions = new AnimationContainer(squadron.MaxEnemies);
            explosionStrides = ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));

            // score
            score = new Score(new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));

            // endGameText
            endGameText = new Text(string.Format("Game Over!"), new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f));
            endGameText.SetFontSize(1000);
            endGameText.SetColor(new Vec3I(0, 128, 255));
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    eventBus.RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "START_MOVE_RIGHT"
                    });
                    break;
                case KeyboardKey.Left:
                    eventBus.RegisterEvent (new GameEvent {
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
                    playerShots.AddEntity(new PlayerShot(player.GetPosition(), playerShotImage));
                break;

                case KeyboardKey.Right:
                    eventBus.RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "STOP_MOVE_RIGHT"
                    });
                break;

                case KeyboardKey.Left:
                    eventBus.RegisterEvent (new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "STOP_MOVE_LEFT"
                    });
                break;

                case KeyboardKey.Escape:
                    window.CloseWindow();
                break;
            }
        }

        public void ProcessEvent(GameEvent gameEvent) {
            System.Console.WriteLine(gameEvent.Message);
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {            
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(key);
                    break;
            }
        }

        public void InitializeSquadronAndStrategy() {
            Random rand = new Random();
            int squadronNumber = rand.Next(3);
            int strategyNumber = rand.Next(3);
            
            squadron = null;
            switch (squadronNumber) {
                case 0:
                    squadron = new PyramidSquadron();
                    break;
                case 1:
                    squadron = new RoundSquadron();
                    break;
                case 2: 
                    squadron = new SquareSquadron();
                    break;
            }
            
            squadron.CreateEnemies(enemyStridesBlue, enemyStridesGreen, speedIncrease);

            strategy = null;
            switch (strategyNumber) {
                case 0:
                    strategy = new NoMove();
                    break;
                case 1:
                    strategy = new ZigZagDown();
                    break;
                case 2: 
                    strategy = new Down();
                    break;
            }
        }

        public override void Render() {
            window.Clear();
            score.RenderScore();
            if (gameOver) {
                endGameText.RenderText();
            } else {
                player.Render();
                squadron.Enemies.RenderEntities();
                enemyExplosions.RenderAnimations();
                playerShots.RenderEntities();
            }
        }

        public override void Update() {
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            player.Move();
            IterateShots();
            strategy.MoveEnemies(squadron.Enemies);
        }

        private void IterateShots() {
            playerShots.Iterate(shot => {
                DynamicShape dynamicShot = shot.Shape.AsDynamicShape();
                dynamicShot.Direction.X = 0.0f;
                dynamicShot.Direction.Y = 0.02f;

                dynamicShot.Move();
            
                Vec2F pos = shot.Shape.Position;
                if (!(0.0f <= pos.X && pos.X <= 1.0f && 0.0f <= pos.Y && pos.Y <= 1.0f)) {
                    // if shot is out of bounds, delete shot
                    shot.DeleteEntity();
                    shot = null;
                    
                } 
                else if (squadron.Enemies.CountEntities() == 0) {
                    rounds++;
                    speedIncrease += 0.0003f;
                    
                    InitializeSquadronAndStrategy();
                    
                }
                else {
                    squadron.Enemies.Iterate(enemy => {
                        bool collision = CollisionDetection.Aabb(dynamicShot, enemy.Shape).Collision;

                        if (collision) {
                            shot.DeleteEntity();                            
                            enemy.DecreaseHitpoints();

                            if (enemy.Hitpoints <= 3 && !enemy.Enraged) {
                                enemy.SetEnragedToTrue();
                            }

                            if (enemy.Hitpoints == 0) {
                                enemy.DeleteEntity(); 
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                score.AddPoints();
                            }
                        }

                        if (enemy.Shape.Position.Y <= 0.0f) {
                            gameOver = true;
                        }
                    });
                }
            });
        }

        public void AddExplosion(Vec2F position, Vec2F extent) {
            StationaryShape shape = new(position.X, position.Y, extent.X, extent.Y);
            ImageStride stride = new(EXPLOSION_LENGTH_MS / 8, explosionStrides);

            enemyExplosions.AddAnimation(shape, EXPLOSION_LENGTH_MS, stride);
        }
    }
}
