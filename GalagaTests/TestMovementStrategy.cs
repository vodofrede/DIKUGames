// using System.IO;
// using DIKUArcade.Graphics;
// using DIKUArcade.Math;
// using Galaga;
// using Galaga.MovementStrategy;
// using Galaga.Squadron;
// using NUnit.Framework;
// using DIKUArcade.GUI;


// namespace GalagaTests;

// [TestFixture]
// public class TestMovementStrategy {

//     ISquadron squadron;
//     IMovementStrategy strategy;
//     float speed;

//     [SetUp]
//     public void Setup() {
//         Window.CreateOpenGLContext();

//         var enemyStride = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
//         var alternativeEnemyStride = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "GreenMonster.png"));

//         speed = 0.003f;

//         squadron = new SquareSquadron();
//         squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
//     }

//     [Test]
//     public void TestDownStrategy() {
//         strategy = new Down();
//         strategy.MoveEnemies(squadron.Enemies);

//         foreach (Enemy enemy in squadron.Enemies)
//         {
//             Assert.AreEqual(enemy.StartingPosition.X, enemy.Shape.Position.X);
//             Assert.AreEqual(enemy.StartingPosition.Y, enemy.Shape.Position.Y + speed);
//         }
//     }

//     [Test]
//     public void TestNoMoveStrategy() {
//         strategy = new NoMove();
//         strategy.MoveEnemies(squadron.Enemies);

//         foreach (Enemy enemy in squadron.Enemies) {
//             Assert.AreEqual(enemy.StartingPosition, enemy.Shape.Position);
//         }
//     }

//     [Test]
//     public void TestZigZagDownStrategy() {
//         strategy = new ZigZagDown();
//         strategy.MoveEnemies(squadron.Enemies);

//         foreach (Enemy enemy in squadron.Enemies)
//         {
//             Assert.AreNotEqual(enemy.StartingPosition, enemy.Shape.Position);
//         }
//     }
// }