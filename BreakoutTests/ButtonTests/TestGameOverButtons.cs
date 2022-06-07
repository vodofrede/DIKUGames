using Breakout.BreakoutStates;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestGameOverButtons {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestNext() {
            var button = GameOverButton.MainMenu;
            Assert.AreEqual(button = button.Next(), GameOverButton.Exit);
            Assert.AreEqual(button = button.Next(), GameOverButton.Exit);
            Assert.AreEqual(button = button.Prev(), GameOverButton.MainMenu);
            Assert.AreEqual(_ = button.Prev(), GameOverButton.MainMenu);
        }
    }
}