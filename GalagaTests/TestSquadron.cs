using System.Collections.Generic;
using System.IO;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using Galaga.Squadron;
using NUnit.Framework;

namespace GalagaTests {

    [TestFixture]
    public class TestSquadron {
        private ISquadron? squadron;
        private readonly List<Image> enemyStride = ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png"));
        private readonly List<Image> alternativeEnemyStride = ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png"));
        private readonly float speed = 0.003f;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
            squadron = new PyramidSquadron();
        }

        [Test]
        public void TestPyramidSquadron() {

            Assert.AreEqual(0, squadron?.Enemies.CountEntities());
            squadron?.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
            Assert.AreEqual(squadron?.MaxEnemies, squadron?.Enemies.CountEntities());
        }

        [Test]
        public void TestSquareSquadron() {
            Assert.AreEqual(0, squadron?.Enemies.CountEntities());
            squadron?.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
            Assert.AreEqual(squadron?.MaxEnemies, squadron?.Enemies.CountEntities());
        }

        [Test]
        public void TestRoundSquadron() {
            Assert.AreEqual(0, squadron?.Enemies.CountEntities());
            squadron?.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
            Assert.AreEqual(squadron?.MaxEnemies, squadron?.Enemies.CountEntities());
        }
    }
}