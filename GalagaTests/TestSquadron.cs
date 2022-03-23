// using System.IO;
// using System.Collections.Generic;
// using DIKUArcade.Graphics;
// using Galaga.Squadron;
// using NUnit.Framework;
// using DIKUArcade.GUI;

// namespace GalagaTests {

// [TestFixture]
// public class TestSquadron {

//     ISquadron squadron;

//     List<Image> enemyStride = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
//     List<Image> alternativeEnemyStride = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "GreenMonster.png"));

//     float speed = 0.003f;

//     [SetUp]
//     public void Setup() {
//         Window.CreateOpenGLContext();

//         var squadron = new PyramidSquadron();
//         squadron.CreateEnemies(enemyStride, alternativeEnemyStride, 0.003f);
//     }

//     [Test]
//     public void TestPyramidSquadron() {
//         squadron = new PyramidSquadron();
//         Assert.AreEqual(0, squadron.Enemies.CountEntities());
//         squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
//         Assert.AreEqual(squadron.MaxEnemies, squadron.Enemies.CountEntities());
//     }

//     [Test]
//     public void TestSquareSquadron() {
//         squadron = new SquareSquadron();
//         Assert.AreEqual(0, squadron.Enemies.CountEntities());
//         squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
//         Assert.AreEqual(squadron.MaxEnemies, squadron.Enemies.CountEntities());
//     }

//     [Test]
//     public void TestRoundSquadron() {
//         squadron = new RoundSquadron();
//         Assert.AreEqual(0, squadron.Enemies.CountEntities());
//         squadron.CreateEnemies(enemyStride, alternativeEnemyStride, speed);
//         Assert.AreEqual(squadron.MaxEnemies, squadron.Enemies.CountEntities());
//     }
// }
// }