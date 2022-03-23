// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Numerics;
// using DIKUArcade.Events;
// using DIKUArcade.GUI;
// using Galaga;
// using Galaga.GalagaStates;
// using NUnit.Framework;

// namespace GalagaTests {
//     [TestFixture]
//     public class StateMachineTesting {
//         private GameEventBus? eventBus;
//         private StateMachine? stateMachine;

//         [SetUp]
//         public void InitiateStateMachine() {
//             Window.CreateOpenGLContext();

//             // (1) Initialize a GalagaBus with proper GameEventTypes
//             eventBus = GalagaBus.GetBus();
//             eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.GameStateEvent });

//             // (2) Instantiate the StateMachine
//             stateMachine = new StateMachine();

//             // (3) Subscribe the GalagaBus to proper GameEventTypes
//             //  and GameEventProcessors
//             eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
//         } 

        
//         [Test]
//         public void TestInitialState() {
//             Assert.IsInstanceOf<MainMenu>(stateMachine?.ActiveState);
//         }

//         [Test]
//         public void TestMainMenuToGameRunning() {
//             GalagaBus.GetBus().RegisterEvent(
//                 new GameEvent{
//                     EventType = GameEventType.GameStateEvent,
//                     Message =  "CHANGE_STATE",
//                     StringArg1 = "GAME_RUNNING"
//                 }
//             );

//             GalagaBus.GetBus().ProcessEventsSequentially();

//             Assert.IsInstanceOf<GameRunning>(stateMachine?.ActiveState);
//         }

//         [Test]
//         public void TestGamePaused() {
//             GalagaBus.GetBus().RegisterEvent(
//                 new GameEvent{
//                     EventType = GameEventType.GameStateEvent,
//                     Message = "CHANGE_STATE",
//                     StringArg1 = "GAME_PAUSED"
//                 }
//             );

//             GalagaBus.GetBus().ProcessEventsSequentially();
//             Assert.IsInstanceOf<GamePaused>(stateMachine?.ActiveState);
//         }

//         [Test]
//         public void TestGameQuitToMainMenu() {
//             GalagaBus.GetBus().RegisterEvent(
//                 new GameEvent {
//                     EventType = GameEventType.GameStateEvent,
//                     Message = "CHANGE_STATE",
//                     StringArg1 = "MAIN_MENU"
//                 }
//             );

//             GalagaBus.GetBus().ProcessEventsSequentially();
//             Assert.IsInstanceOf<MainMenu>(stateMachine?.ActiveState);
//         }
//     }
// }