using Cation.Core.GameInstaller;

namespace Cation.Test.Core;

public class MinecraftVersionListTest
{
    [Test, Order(1)]
    public async Task GetVersionList()
    {
        var versionList = await MinecraftGameDownloader.GetVersionListAsync();
        Assert.That(versionList, Is.Not.Null);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(versionList.Latest.Release, Is.Not.Empty);
            Assert.That(versionList.Latest.Snapshot, Is.Not.Empty);
            Assert.That(versionList.Versions, Is.Not.Empty);
        }
    }

    [Test, Order(2)]
    public async Task GetLatestVersion()
    {
        var latestVersion = await MinecraftGameDownloader.GetLatestVersionAsync();
        Assert.That(latestVersion, Is.Not.Null);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(latestVersion.Id, Is.Not.Empty);
            Assert.That(latestVersion.Type, Is.Not.Empty);
            Assert.That(latestVersion.Url, Is.Not.Empty);
            Assert.That(latestVersion.Time, Is.Not.Empty);
            Assert.That(latestVersion.ReleaseTime, Is.Not.Empty);
            Assert.That(latestVersion.Sha1, Is.Not.Empty);
            Assert.That(latestVersion.ComplianceLevel, Is.Zero.Or.EqualTo(1));
        }
    }
}
