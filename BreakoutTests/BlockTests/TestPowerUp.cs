using System.IO;
using Breakout.Blocks;
using Breakout.BreakoutStates;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestPowerUp {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestPowerUpWorks() {
            var lives = 0;
            var powerUp = new PowerUp(new Vec2F(0.0f, 0.0f), new Image(Path.Combine("Assets", "Images", "heart_filled.png")), () => lives++);

            powerUp.Update();
            Assert.AreNotEqual(powerUp.Shape.Position.Y, 0.0f);

            powerUp.Effect();
            Assert.AreEqual(1, lives);
        }
    }
}