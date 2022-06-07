using Breakout.BreakoutStates;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestGamePausedButtons {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestNext() {
            var button = GamePausedButton.Continue;
            Assert.AreEqual(GamePausedButton.MainMenu, button = button.Next());
            Assert.AreEqual(GamePausedButton.Exit, button = button.Next());
            Assert.AreEqual(GamePausedButton.Exit, button = button.Next());

            Assert.AreEqual(GamePausedButton.MainMenu, button = button.Prev());
            Assert.AreEqual(GamePausedButton.Continue, button = button.Prev());
            Assert.AreEqual(GamePausedButton.Continue, button.Prev());
        }
    }
}