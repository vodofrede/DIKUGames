using System.IO;
using System.Linq.Expressions;
using Breakout;
using NUnit.Framework;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace BreakoutTests;

public class TestBlock {
    Block block;

    [SetUp]
    public void Setup() {
        block = new Block(
            BlockType.Standard, 
            new StationaryShape(new Vec2F(.4f, 0.4f), new Vec2F(0.1f, 0.1f)),
            new Image(Path.Combine("Assets", "Images", "green-block.png"))
        );
    }

    [Test]
    public void TestIsEntity() {
        // test satisfies R.1
        Assert.True(typeof(Block).IsSubtypeOf(typeof(Entity)));
    }

    [Test]
    public void TestHasValueAndHealth() {
        // test satisfies R.2
        Assert.True(block.GetHitpoints() != null);
        Assert.True(block.GetValue() != null);
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
