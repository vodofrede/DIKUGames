using Breakout.BreakoutStates;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestMainMenuButtons {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestNext() {
            var button = MainMenuButton.Play;
            Assert.AreEqual(button = button.Next(), MainMenuButton.Exit);
            Assert.AreEqual(button = button.Next(), MainMenuButton.Exit);

            Assert.AreEqual(button = button.Prev(), MainMenuButton.Play);
            Assert.AreEqual(_ = button.Prev(), MainMenuButton.Play);
        }
    }
}