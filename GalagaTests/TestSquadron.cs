using NUnit.Framework;

namespace Galaga;

[TestFixture]
public class TestSquadron {
    [SetUp]
    public void Setup() {

        enemyStridesBlue = ImageStride.CreateStrides(4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        enemyStridesGreen = ImageStride.CreateStrides(2, Path.Combine("Assets", "Images", "GreenMonster.png"));

        Squadron squadron = new PyramidSquadron();
        squadron.CreateEnemies(enemyStridesBlue, enemyStridesGreen, 0.003f);
    }

    [Test]
    public void Test1() {
        Assert.Pass();
    }
}