using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using Galaga;
using NUnit.Framework;

namespace GalagaTests;

[TestFixture]
public class TestEnemy {

    IBaseImage? enemyStride;
    IBaseImage? alternativeEnemyStride;

    Enemy? enemy;

    [SetUp]
    public void Setup() {
        Window.CreateOpenGLContext();

        enemyStride = new ImageStride(80, ImageStride.CreateStrides(4, Path.Combine("..", "Galaga", "Assets", "Images", "BlueMonster.png")));
        alternativeEnemyStride = new ImageStride(80, ImageStride.CreateStrides(2, Path.Combine("..", "Galaga", "Assets", "Images", "GreenMonster.png")));

        enemy = new Enemy(
            new DynamicShape(0.3f, 0.9f, 0.1f, 0.1f),
            enemyStride,
            alternativeEnemyStride,
            1.0f
        );
    }

    [Test]
    public void TestEnraged() {
        enemy?.SetEnragedToTrue();
        Assert.AreEqual(4.5f, enemy?.Speed);
        Assert.True(enemy?.Enraged);
    }

    [Test]
    public void TestDecreaseHitpoints() {
        var initialHitpoints = enemy?.Hitpoints;
        enemy?.DecreaseHitpoints();
        Assert.Greater(initialHitpoints, enemy?.Hitpoints);
    }
}