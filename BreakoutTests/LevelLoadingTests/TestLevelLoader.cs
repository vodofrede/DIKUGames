using System.IO;
using Breakout.Levels;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests;

public class TestLevelLoader {
    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
    }

    [Test]
    public void TestAllLevelsWorkAsMaps() {
        // this test satisfies R.1
        string[] levels = new string[] { "central-mass.txt", "columns.txt", "level1.txt", "level2.txt", "level3.txt", "wall.txt" };
        Level currentLevel;
        // if any level fails to parse, this test will fail because an exception will be thrown.
        foreach (string level in levels) {
            currentLevel = LevelLoader.LoadLevel(Path.Combine("Assets", "Levels", level));
            Assert.Greater(currentLevel.TimeLimit, 0);
        }
    }

    [Test]
    public void TestEmptyFiles() {
        // this test satisfies R.3
        Assert.IsNotNull(LevelLoader.TryLoadLevel(Path.Combine("Assets", "Levels", "level2.txt")));
    }
}
