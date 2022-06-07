using Breakout;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestTextField {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestTextFieldChanges() {
            var a = 2;
            var textField = new TextField(() => "" + a, new Vec2F(1.0f, 1.0f), new Vec2F(0.2f, 0.2f));

            // it is currently not possible to test this as the "text" property of the text field is protected.
        }
    }
}