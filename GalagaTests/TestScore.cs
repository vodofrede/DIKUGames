using DIKUArcade.GUI;
using DIKUArcade.Math;
using Galaga;
using NUnit.Framework;

namespace GalagaTests {
    [TestFixture]
    public class TestScore {
        private Score? score;

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            score = new Score(
                new Vec2F(1.0f, 1.0f),
                new Vec2F(1.0f, 1.0f)
            );
        }

        [Test]
        public void TestAddPoints() {
            Assert.True(score?.Points == 0);
            score?.AddPoints();
            Assert.True(score?.Points == 1);
        }
    }
}