using DIKUArcade.Events;
using DIKUArcade.GUI;
using Galaga.GalagaStates;
using NUnit.Framework;

namespace GalagaTests {
    [TestFixture]
    public class StateMachineTesting {
        private StateMachine? stateMachine;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            stateMachine = new StateMachine();

            // eventBus.Subscribe(GameEventType.GameStateEvent, this);
        }


        [Test]
        public void TestInitialState() {
            Assert.IsInstanceOf<MainMenu>(stateMachine?.ActiveState);
        }

        [Test]
        public void TestMainMenuToGameRunning() {
            stateMachine?.ProcessEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "GAME_RUNNING"
                }
            );

            Assert.IsInstanceOf<GameRunning>(stateMachine?.ActiveState);
        }

        [Test]
        public void TestGamePaused() {
            stateMachine?.ProcessEvent(new GameEvent {
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            });

            Assert.IsInstanceOf<GamePaused>(stateMachine?.ActiveState);
        }

        [Test]
        public void TestGameQuitToMainMenu() {
            stateMachine?.ProcessEvent(
                new GameEvent {
                    EventType = GameEventType.GameStateEvent,
                    Message = "CHANGE_STATE",
                    StringArg1 = "MAIN_MENU"
                }
            );

            Assert.IsInstanceOf<MainMenu>(stateMachine?.ActiveState);
        }
    }
}