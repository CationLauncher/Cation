using Cation.Core.GameInstaller;

namespace Cation.Test;

public class Tests
{
    [Test]
    public async Task GetVersionList()
    {
        var versionList = await MinecraftGameDownloader.GetVersionListAsync();
        Assert.That(versionList, Is.Not.Null);
    }
}
