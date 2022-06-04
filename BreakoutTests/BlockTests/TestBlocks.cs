using System;
using System.IO;
using Breakout;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;
using NUnit.Framework;

namespace BreakoutTests;

public class TestBlock {
    Block? block;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();
    }

    public static void MandatoryBlockRequirements(Block block) {
        Assert.True(typeof(Block).IsSubclassOf(typeof(Entity)));
        Assert.IsNotNull(block.Hitpoints);
        Assert.IsNotNull(block.Value);
    }

    public static void HealthCanDecrement(Block block) {
        var hitpoints = block.Hitpoints;
        block.OnHit();
        Assert.True(block.Hitpoints == 0 || block.Hitpoints == hitpoints - 1);
    }

    [Test]
    public void TestNormalBlock() {
        block = new Block(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "None");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestDoubleSize() {
        block = new DoubleSize(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "DoubleSize");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestExtraLife() {
        block = new ExtraLife(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "ExtraLife");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestHardened() {
        block = new Hardened(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.None);
        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "None");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestHungry() {
        block = new Hungry(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.None);
        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "Hungry");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestInvincible() {
        block = new Invincible(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "Invincible");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestSpeedPowerUp() {
        block = new SpeedPowerUp(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "SpeedPowerUp");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }

    [Test]
    public void TestUnbreakable() {
        block = new Unbreakable(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.None);
        Assert.True(!block.IsBreakable());
        Assert.AreEqual(block.Effect, "None");
        MandatoryBlockRequirements(block);
    }

    [Test]
    public void TestWidePowerUp() {
        block = new WidePowerUp(new Vec2F(0.0f, 0.0f), "blue-block.png");

        Assert.AreEqual(block.OnHit(), BlockAction.Destroy);
        Assert.AreEqual(block.Effect, "WidePowerUp");
        MandatoryBlockRequirements(block);
        HealthCanDecrement(block);
    }
}
