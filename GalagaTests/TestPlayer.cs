using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using Galaga;
using NUnit.Framework;

// you should test that your player is moving as expected and that the obligations imposed by the ISquadron and
// IMovementStrategy interfaces are being adequately met by the classes implementing them. Furthermore any relevant methods and their effect on
// the enrage-state of the Enemy class should be tested.
// In your test project go ahead and create classes for all of this, e.g. the classes
// TestPlayer.cs, TestSquadron.cs, TestMovementStrategy.cs,
// TestEnemy.cs and TestScore.cs. Note that when testing the movement
// of the player, you have to register appropriate events and process them as
// well create an OpenGLContext in the [SetUp]. An example of this is shown
// in Figure 4 in the context of testing the StateMachine.

namespace GalagaTests {

    [TestFixture]
    public class TestPlayer {

        Player? player;
        GameEventBus galagaBus = GalagaBus.GetBus();
    
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            player = new Player(
                new DynamicShape(new Vec2F(0.4f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "Player.png"))
            );

            galagaBus.Subscribe(GameEventType.PlayerEvent, player);
        }

        [Test]
        public void TestPlayerMovementRight() {

            for (var i = 0; i < 10; i++)
            {
                galagaBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    From = this,
                    To = player,
                    Message = "START_MOVE_RIGHT"
                });

                galagaBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    From = this,
                    To = player,
                    Message = "STOP_MOVE_RIGHT"
                });
            }

            Assert.AreEqual(player.GetPosition(), new Vec2F(0.5f, 0.1f));
        }

        [Test]
        public void TestPlayerMovementLeft() {

            for (var i = 0; i < 10; i++)
            {
                galagaBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    From = this,
                    To = player,
                    Message = "START_MOVE_LEFT"
                });

                galagaBus.RegisterEvent(new GameEvent {
                    EventType = GameEventType.PlayerEvent,
                    From = this,
                    To = player,
                    Message = "STOP_MOVE_LEFT"
                });
            }

            Assert.AreEqual(player.GetPosition(), new Vec2F(0.3f, 0.1f));
        }
    }
}


