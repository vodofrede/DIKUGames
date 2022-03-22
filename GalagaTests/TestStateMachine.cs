using DIKUArcade.Events;
using DIKUArcade.GUI;
using Galaga;
using NUnit.Framework;
using System.IO;

namespace GalagaTests {

    [TestFixture]
    public class StateMachineTesting {

        private StateMachine stateMachine;

    [SetUp]
    public void InitiateStateMachine() {
        Window.CreateOpenGLContext();

        // Here you should:
        // (1) Initialize a GalagaBus with proper GameEventTypes
        // (2) Instantiate the StateMachine
        // (3) Subscribe the GalagaBus to proper GameEventTypes
        //  and GameEventProcessors

    } 

    
    [Test]
    public void TestInitialState() {
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<MainMenu>());
    }

    [Test]
    public void TestEventGamePaused() {
        GalagaBus.GetBus().RegisterEvent(
            new GameEvent{
                EventType = GameEventType.GameStateEvent,
                Message = "CHANGE_STATE",
                StringArg1 = "GAME_PAUSED"
            }
        );

        GalagaBus.GetBus().ProcessEventsSequentially();
        Assert.That(stateMachine.ActiveState, Is.InstanceOf<GamePaused>());
    }

    }
}