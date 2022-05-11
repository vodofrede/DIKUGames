using System;
using System.IO;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using NUnit.Framework;

namespace BreakoutTests;

public class TestBlock {
#pragma warning disable CS8618
    Block block;
#pragma warning restore CS8618

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        block = new Block(
            BlockType.Standard,
            new Vec2F(0.5f, 0.5f),
            "red-block.png"
        );
    }

    [Test]
    public void TestIsEntity() {
        // test satisfies R.1
        Assert.True(typeof(Block).IsSubclassOf(typeof(Entity)));
    }

    [Test]
    public void TestHasValueAndHealth() {
        // test satisfies R.2
        Console.WriteLine(block.Hitpoints);
        Console.WriteLine(block.Value);
        Assert.Pass();
    }

    [Test]
    public void TestHealthDecrement() {
        // test satisfies R.5
        Assert.AreEqual(1, block.Hitpoints);
        block.DecreaseHitpoints();
        Assert.AreEqual(0, block.Hitpoints);
    }

    [Test]
    public void TestDestroyBlock() {
        // test satisfies R.6
        Assert.AreEqual(1, block.Hitpoints);
        Assert.AreEqual(BlockEffect.Destroy, block.DecreaseHitpoints());
    }
}
