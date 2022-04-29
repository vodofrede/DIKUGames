using System.Collections.Generic;
using System.IO;
using Breakout;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using NUnit.Framework;

namespace BreakoutTests;

public class TestFileLoader {
    [SetUp]
    public void Setup() {}

    [Test]
    public void TestAllLevelsWorkAsMaps() {
        // this test satisfies R.1
        string[] levels = Directory.GetFiles(Path.Combine("Assets", "Levels"));

        // if any level fails to parse, this test will fail because an exception will be thrown.
        foreach (string level in levels) {
            FileLoader.ParseFile(level);
        }

        Assert.Pass();
    }

    [Test]
    public void TestMapParsedCorrectly() {
        // this test satisfies R.2
        Map map = FileLoader.ParseFile(Path.Combine("Assets", "Levels", "level2"));

        Assert.AreEqual("LEVEL 2", map.GetName());
        Assert.AreEqual(180, map.GetTimeLimit());
        Assert.AreEqual(72, map.GetBlocks().CountEntities());

        Assert.Pass();
    }

    [Test]
    public void TestEmptyFiles() {
        // this test satisfies R.3
        FileLoader.TryParseFile(Path.Combine("Assets", "Levels", "level2.txt"));

        Assert.Pass();
    }
}
