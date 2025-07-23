using Cation.Core.Forge;

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
