using System.IO;
using System.Linq.Expressions;
using NUnit.Framework;
using Breakout;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using System;

namespace BreakoutTests;

public class TestPlayer {
    Player player;

    [SetUp]
    public void Setup() {
        player = new Player(
            new Image(Path.Combine("Assets", "Images", "player.png"))
        );
    }

    [Test]
    public void TestStartsAtCenter() {
        // test satisfies R.1
        Assert.True(0.4f < player.Shape.Position.X && player.Shape.Position.X < 0.6f);
    }

    [Test]
    public void TestPlayerMove() {
        // test satisfies R.2
        var startingPosition = player.Shape.Position.X;
        player.SetMoveRight(true);
        player.Move();
        Assert.Less(startingPosition, player.Shape.Position.X);

        startingPosition = player.Shape.Position.X;
        player.SetMoveRight(false);
        player.SetMoveLeft(true);
        player.Move();
        Assert.Greater(startingPosition, player.Shape.Position.X);
    }

    [Test]
    public void TestNoExitScreenRight() {
        // test satisfies R.3
        player.SetMoveRight(true);
        for (int i; i < 100; i++) {
            player.Move();   
        }  
        Assert.LessOrEqual(player.Shape.Position.X, 1.0f - player.Shape.Extent.X);
    }

    [Test]
    public void TestNoExitScreenLeft() {
        // test satisfies R.3
        for (var i; i < 100; i++) {
            player.SetMoveLeft(true);
            player.Move();
        }
        Assert.GreaterOrEqual(player.Shape.Position.X, 0.0f);
    }

    [Test]
    public void TestNoStutteringWhenMovingPlayerAroundTheScreenInTheHorizontalDirection() {
        // test satisfies R.4
        var startingPosition = player.Shape.Position.X;
        player.Move();
        Assert.Greater(0.1f, Math.Abs(startingPosition - player.Shape.Position.X));
    }

    [Test]
    public void TestIsEntity() {
        // test satisfies R.5
        Assert.True(typeof(Player).IsSubtypeOf(typeof(Entity)));
    }

    [Test]
    public void TestRectangularShapeDefault() {
        // test satisfies R.6
        var shape = new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.02f));
        Assert.AreEqual(shape, player.Shape);
    }

    [Test]
    public void TestPlayerInBottomHalfOfScreen() {
        // test satisfies R.7
        Assert.GreaterOrEqual(0.5f, player.Shape.Position.Y);
    }
}
