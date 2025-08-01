using Cation.Core.ModLoader.Forge;

namespace Cation.Test.Core;

public class ForgeVersionListTest
{
    [Test]
    public async Task GetForgeVersionList()
    {
        var result = await ForgeVersionList.GetForgeVersionListAsync();
        Assert.That(result, Is.Not.Empty);
    }
}
