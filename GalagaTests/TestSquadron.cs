using DIKUArcade.Graphics;
using Galaga.Squadron;
using NUnit.Framework;
using System.IO;

namespace Galaga {

[TestFixture]
public class TestSquadron {
    [SetUp]
    public void Setup() {

        ImageStride enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        ImageStride enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "GreenMonster.png"));

        var squadron = new PyramidSquadron();
        squadron.CreateEnemies(enemyStridesBlue, enemyStridesGreen, 0.003f);
    }

    [Test]
    public void Test1() {
        Assert.Pass();
    }
}
}