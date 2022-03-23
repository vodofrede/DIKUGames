using System;
using Galaga;
using Galaga.GalagaStates;
using NUnit.Framework;

namespace GalagaTests;

[TestFixture]
public class TestStateTransformer {
    [SetUp]
    public void Setup() { }

    [Test]
    public void TestTransformStateToString() {
        Assert.AreEqual(GameStateType.MainMenu, StateTransformer.TransformStringToState("MAIN_MENU"));
        Assert.AreEqual(GameStateType.GameRunning, StateTransformer.TransformStringToState("GAME_RUNNING"));
        Assert.AreEqual(GameStateType.GamePaused, StateTransformer.TransformStringToState("GAME_PAUSED"));
        Assert.Throws<ArgumentException>(() => StateTransformer.TransformStringToState("GOOD SHIT M8"));
    }

    [Test]
    public void TestTransformStringToState() {
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.MainMenu), "MAIN_MENU");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GameRunning), "GAME_RUNNING");
        Assert.AreEqual(StateTransformer.TransformStateToString(GameStateType.GamePaused), "GAME_PAUSED");
    }
}