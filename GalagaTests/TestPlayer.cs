using System.Collections.Generic;
using System.IO;
using System.Threading;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using Galaga;
using NUnit.Framework;

namespace GalagaTests {

    [TestFixture]
    public class TestPlayer {
        private Player? player;
        private readonly GameEventBus eventBus = GalagaBus.GetBus();
        private bool initialized;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            if (!initialized) {
                eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.GameStateEvent });
            }
            initialized = true;

            player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("..", "Galaga", "Assets", "Images", "Player.png"))
            );

            eventBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        [Test]
        public void TestPlayerMovementRight() {
            Vec2F? startingPosition = player?.GetPosition();

            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "START_MOVE_RIGHT"
            });

            void Work() {
                Thread.Sleep(1000);
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_MOVE_RIGHT"
                });
                Assert.Greater(startingPosition?.X, player?.GetPosition().X);
            }

            Thread thread = new(Work);
        }

        [Test]
        public void TestPlayerMovementLeft() {
            Vec2F? startingPosition = player?.GetPosition();

            eventBus.RegisterEvent(new GameEvent {
                EventType = GameEventType.PlayerEvent,
                Message = "START_MOVE_LEFT"
            });

            void Work() {
                Thread.Sleep(1000);
                eventBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    Message = "STOP_MOVE_LEFT"
                });
                Assert.Less(startingPosition?.X, player?.GetPosition().X);
            }

            Thread thread = new(Work);
        }

        [Test]
        public void TestPlayerShoot() {

        }
    }
}


