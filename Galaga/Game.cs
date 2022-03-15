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
using Galaga.Squadron;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;

        // game state
        private int score = 0;
        private bool gameOver = false;

        // contained entities
        private Player player;
        private ISquadron squadron;
        // private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;

        // images & animations
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private List<Image> enemyStridesGreen;
        private List<Image> enemyStridesBlue;
        private const int EXPLOSION_LENGTH_MS = 500;

        // text
        private Text scoreText;
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
            squadron.CreateEnemies(enemyStridesBlue, enemyStridesGreen);
            
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
            scoreText = new Text(string.Format("Score: " + score.ToString()), new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f));
            scoreText.SetColor(new Vec3I(0, 128, 255));
            scoreText.SetFontSize(200);

            // endGameText
            endGameText = new Text(string.Format("You won!"), new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f));
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

        public override void Render() {
            window.Clear();
            player.Render();
            squadron.Enemies.RenderEntities();
            enemyExplosions.RenderAnimations();
            playerShots.RenderEntities();
            if (gameOver) endGameText.RenderText();
            if (!gameOver) scoreText.RenderText();
        }

        public override void Update() {
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            player.Move();
            IterateShots();
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
                    gameOver = true;
                }
                else {
                    squadron.Enemies.Iterate(enemy => {
                        bool collision = CollisionDetection.Aabb(dynamicShot, enemy.Shape).Collision;

                        if (collision) {
                            shot.DeleteEntity();                            
                            enemy.DecreaseHitpoints();

                            if (enemy.Hitpoints <= 5) {
                                enemy.SetEnragedToTrue();
                            }

                            if (enemy.Hitpoints == 0) {
                                enemy.DeleteEntity(); 
                                AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                                score += 1;
                                scoreText.SetText(string.Format("Score: " + score.ToString()));   
                            }
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
