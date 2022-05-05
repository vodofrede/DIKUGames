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
            new StationaryShape(new Vec2F(.4f, 0.4f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "green-block.png"))
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
        block.GetHitpoints();
        block.GetValue();
        Assert.Pass();
    }

    [Test]
    public void TestHealthDecrement() {
        // test satisfies R.5
        Assert.AreEqual(1, block.GetHitpoints());
        block.DecreaseHitpoints();
        Assert.AreEqual(0, block.GetHitpoints());
    }

    [Test]
    public void TestDestroyBlock() {
        // test satisfies R.6
        Assert.AreEqual(1, block.GetHitpoints());
        Assert.AreEqual(BlockEffect.Destroy, block.DecreaseHitpoints());
    }
}
