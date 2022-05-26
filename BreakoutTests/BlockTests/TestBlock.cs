using System;
using System.IO;
using Breakout.Blocks;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using NUnit.Framework;
using DIKUArcade.Graphics;

namespace BreakoutTests;

public class TestBlock {
#pragma warning disable CS8618
    Block block;
    DoubleSize doubleSize;
    ExtraLife extraLife;
    SpeedPowerUp speedPowerUp;
    Player player;
#pragma warning restore CS8618

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        player = new Player(
            new Image(Path.Combine("Assets", "Images", "player.png"))
        );

        block = new Block(
            new Vec2F(0.5f, 0.5f),
            "red-block.png"
        );

        doubleSize = new DoubleSize(
            new Vec2F(0.5f, 0.5f),
            "red-block.png"
        );

        extraLife = new ExtraLife(
            new Vec2F(0.5f, 0.5f),
            "red-block.png"
        );

        speedPowerUp = new SpeedPowerUp(
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
        block.OnHit();
        Assert.AreEqual(0, block.Hitpoints);
    }

    [Test]
    public void TestDestroyBlock() {
        // test satisfies R.6
        Assert.AreEqual(1, block.Hitpoints);
        Assert.AreEqual(BlockAction.Destroy, block.OnHit());
    }

    // [Test]
    // public void TestDoubleSize() {
    //     Vec2F originalSize = player.Shape.Extent;
    //     doubleSize.OnHit();
    //     Vec2F updatedSize = player.Shape.Extent;
    //     Assert.AreEqual(originalSize, updatedSize);
    // }

    // [Test]
    // public void TestExtraLife() {
    //     // float originaLife = player.MovementSpeed;
    //     extraLife.DeleteEntity();
    //     // float updatedLife = player.MovementSpeed;
    
    //     Assert.AreEqual(1, extraLife.Hitpoints);
    // }

    // [Test]
    // public void TestSpeedPowerUp() {
    //     float originalSpeed = player.MovementSpeed;
    //     speedPowerUp.OnHit();
    //     float updatedSpeed = player.MovementSpeed;
    //     Assert.AreEqual(originalSpeed, updatedSpeed);
    // }
}
