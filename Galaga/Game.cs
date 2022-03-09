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

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        private GameEventBus eventBus;
        private Player player;
        private EntityContainer<Enemy> enemies;
        private EntityContainer<PlayerShot> playerShots;
        private IBaseImage playerShotImage;
        private AnimationContainer enemyExplosions;
        private List<Image> explosionStrides;
        private int score = 0;
        private Text scoreText;
        private Text endGameText;
        private const int EXPLOSION_LENGTH_MS = 500;
        private bool gameOver = false;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // event bus (only subscribed to InputEvent)
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });

            window.SetKeyEventHandler(KeyHandler);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            // enemy setup
            var images = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
            const int numEnemies = 8;

            enemies = new EntityContainer<Enemy>(numEnemies);
            for (int i = 0; i < numEnemies; i++) {
                enemies.AddEntity(new Enemy(
                    new DynamicShape(0.1f + (float)i * 0.1f, 0.9f, 0.1f, 0.1f),
                    new ImageStride(80, images)));
            }
            
            // player setup
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );

            // player shot setup
            playerShots = new EntityContainer<PlayerShot>();
            playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

            // explosion setup
            enemyExplosions = new AnimationContainer(numEnemies);
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
                    player.SetMoveLeft(false);
                    player.SetMoveRight(true);
                break;
                 
                case KeyboardKey.Left:
                    player.SetMoveRight(false);
                    player.SetMoveLeft(true);                    
                break;
            }
            
        }
        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Space:
                    playerShots.AddEntity(new PlayerShot(player.GetPosition(), playerShotImage));
                break;

                case KeyboardKey.Right:
                    player.SetMoveRight(false);
                break;
                 
                case KeyboardKey.Left:
                    player.SetMoveLeft(false);
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
            enemies.RenderEntities();
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
                else if (enemies.CountEntities() == 0) {
                    gameOver = true;
                }
                else {
                    enemies.Iterate(enemy => {
                        
                        bool collision = CollisionDetection.Aabb(dynamicShot, enemy.Shape).Collision;

                        if (collision) {
                            AddExplosion(enemy.Shape.Position, enemy.Shape.Extent);
                            enemy.DeleteEntity(); shot.DeleteEntity();
                            score += 1;
                            scoreText.SetText(string.Format("Score: " + score.ToString()));   
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
