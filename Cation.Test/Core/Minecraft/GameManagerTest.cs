using Cation.Core.Minecraft;

namespace Cation.Test.Core.Minecraft;

public class GameManagerTest
{
    [Test]
    [Explicit]
    public void GetGameInstances()
    {
        var gameInstances = GameManager.GetGameInstances(null);
        Assert.That(gameInstances, Is.Not.Empty);
    }
}
