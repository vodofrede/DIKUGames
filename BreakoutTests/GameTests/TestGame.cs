// using System;
// using System.IO;
// using Breakout;
// using DIKUArcade.Entities;
// using DIKUArcade.Graphics;
// using DIKUArcade.GUI;
// using DIKUArcade.Math;
// using DIKUArcade.Events;
// using NUnit.Framework;
// using Breakout.BreakoutStates;

// namespace GameTests {
//     public class TestGame {
//         [SetUp]
//         public void Setup() {
//             Window.CreateOpenGLContext();
//             EventBus eventBus = new EventBus();

//             StateMachine stateMachine = new StateMachine(eventBus);
//             stateMachine.AddState(new GameRunning(eventBus));
//             stateMachine.AddState(new GameOver(eventBus));
//             stateMachine.SwitchState("GameRunning");

//             // player setup
//             Player player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
//             eventBus.Subscribe(GameEventType.PlayerEvent, player);
//             eventBus.Subscribe(GameEventType.TimedEvent, this);

//             // other entities
//             Ball ball = new Ball(new Vec2F(0.5f, 0.15f));
//             LevelLoader levelLoader = new LevelLoader();
//             Level level = levelLoader.Next();
//         }


//         [Test]
//         public void TestStartsAtCenter() {
//             // test satisfies R.1
//             Assert.True(0.4f < player.Shape.Position.X && player.Shape.Position.X < 0.6f);
//         }
//     }
// }