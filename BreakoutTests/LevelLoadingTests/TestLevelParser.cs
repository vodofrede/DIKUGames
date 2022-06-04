using System.IO;
using Breakout.Levels;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestLevelParser {
        private const string LEVEL2 = "Map:\n------------\n------------\nhhhhhhhhhhhh\nhhihhhhhhihh\n------------\njjjjjijjjjjj\njjijjjijjijj\n------------\nkkkkkkkkkkkk\nkkkkkkkkkkkk\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\n------------\nMap/\nMeta:\nName: LEVEL 2\nTime: 180\nPowerUp: i\nMeta/\nLegend:\nh) green-block.png\ni) teal-block.png\nj) blue-block.png\nk) brown-block.png\nLegend/";

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();
        }

        [Test]
        public void TestMapParsedCorrectly() {
            // this test satisfies R.2
            Level map = LevelParser.ParseContents(LEVEL2);

            Assert.AreEqual("LEVEL 2", map.Name);
            Assert.AreEqual(180, map.TimeLimit);
            Assert.AreEqual(72, map.Blocks.CountEntities());
        }

        [Test]
        public void TestEmptyContents() {
            Level? level = LevelParser.TryParseContents("");
            Assert.IsNull(level);
        }
    }
}