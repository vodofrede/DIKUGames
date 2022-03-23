using System.Collections.Generic;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using Galaga;
using Galaga.MovementStrategy;
using Galaga.Squadron;
using NUnit.Framework;


namespace GalagaTests {
    [TestFixture]
    public class TestMovementStrategy {
        private List<Image>? enemyStride;
        private List<Image>? alternativeEnemyStride;
        private ISquadron? squadron;
        private IMovementStrategy? strategy;
        private float speed;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            enemyStride = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
            alternativeEnemyStride = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png"));

            speed = 0.003f;
        }

        [Test]
        public void TestDownStrategy() {
            squadron = new SquareSquadron();
            squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);

            strategy = new Down();
            strategy.MoveEnemies(squadron.Enemies);
            strategy.MoveEnemies(squadron.Enemies);
            strategy.MoveEnemies(squadron.Enemies);
            strategy.MoveEnemies(squadron.Enemies);
            strategy.MoveEnemies(squadron.Enemies);

            foreach (Enemy enemy in squadron.Enemies) {
                Assert.AreEqual(enemy.StartingPosition.X, enemy.Shape.Position.X);
                Assert.Less(enemy.Shape.Position.Y, enemy.StartingPosition.Y);
            }
        }

        [Test]
        public void TestNoMoveStrategy() {
            squadron = new SquareSquadron();
            squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);

            strategy = new NoMove();
            strategy.MoveEnemies(squadron.Enemies);

            foreach (Enemy enemy in squadron.Enemies) {
                Assert.AreEqual(enemy.StartingPosition.X, enemy.Shape.Position.X);
                Assert.AreEqual(enemy.StartingPosition.Y, enemy.Shape.Position.Y);
            }
        }

        [Test]
        public void TestZigZagDownStrategy() {
            squadron = new SquareSquadron();
            squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);

            strategy = new ZigZagDown();
            strategy.MoveEnemies(squadron.Enemies);

            foreach (Enemy enemy in squadron.Enemies) {
                Assert.AreNotEqual(enemy.StartingPosition, enemy.Shape.Position);
            }
        }
    }
}