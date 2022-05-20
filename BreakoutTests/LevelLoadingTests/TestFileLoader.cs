using System;
using System.IO;
using Breakout;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests;

public class TestFileLoader {
    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
    }

    [Test]
    public void TestAllLevelsWorkAsMaps() {
        // this test satisfies R.1
        string[] levels = new string[] { "central-mass.txt", "columns.txt", "level1.txt", "level2.txt", "level3.txt", "wall.txt" };

        // if any level fails to parse, this test will fail because an exception will be thrown.
        foreach (string level in levels) {
            LevelLoader.ParseFile(Path.Combine("Assets", "Levels", level));
        }

        Assert.Pass();
    }

    [Test]
    public void TestMapParsedCorrectly() {
        // this test satisfies R.2
        Level map = LevelLoader.ParseFile(Path.Combine("Assets", "Levels", "level2.txt"));

        Assert.AreEqual("LEVEL 2", map.Name);
        Assert.AreEqual(180, map.TimeLimit);
        Assert.AreEqual(72, map.Blocks.CountEntities());

        Assert.Pass();
    }

    [Test]
    public void TestEmptyFiles() {
        // this test satisfies R.3
        LevelLoader.TryParseFile(Path.Combine("Assets", "Levels", "level2.txt"));

        Assert.Pass();
    }
}
