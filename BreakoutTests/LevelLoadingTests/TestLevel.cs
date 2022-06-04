using Breakout.Blocks;
using Breakout.Levels;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestLevel {
        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestLevelHasCorrectFields() {
            var level = new Level("lol", 301, new EntityContainer<Block>() { });
            Assert.AreEqual(level.Name, "lol");
            Assert.AreEqual(level.TimeLimit, 301);
            Assert.True(level.Blocks.CountEntities() == 0);
        }
    }
}