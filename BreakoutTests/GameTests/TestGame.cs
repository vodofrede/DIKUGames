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
// using DIKUArcade.State;

// namespace GameTests {
//     public class TestGame {
//         #pragma warning disable CS8618
//         Player player;
//         Ball ball;
//         Level level;
//         LevelLoader levelLoader;
//         StateMachine stateMachine;
//         // Player player;
//         #pragma warning restore CS8618

//         [SetUp]
//         public void Setup() {
//             Window.CreateOpenGLContext();
//             EventBus eventBus = new EventBus();

//             StateMachine stateMachine = new StateMachine(eventBus);
//             stateMachine.AddState(new GameRunning(eventBus));
//             stateMachine.AddState(new GameOver(eventBus));
//             stateMachine.SwitchState("GameRunning");

//             // player setup
//             player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
//             eventBus.Subscribe(GameEventType.PlayerEvent, player);
//             // eventBus.Subscribe(GameEventType.TimedEvent, this);

//             // other entities
//             ball = new Ball(new Vec2F(0.5f, 0.15f));
//             levelLoader = new LevelLoader();
//             level = levelLoader.Next();
//         }


//         [Test]
//         public void TestPlayerLivesNeverNegative() {
//             // test satisfies R.1
//             GameRunning gameRunning = (GameRunning)stateMachine.ActiveState;
//             Assert.GreaterOrEqual(gameRunning.lives, 0); 
//         }
//     }
// }