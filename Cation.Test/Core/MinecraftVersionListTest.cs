using Cation.Core.GameInstaller;

namespace Cation.Test.Core;

public class Tests
{
    [Test]
    public async Task GetVersionList()
    {
        var versionList = await MinecraftGameDownloader.GetVersionListAsync();
        Assert.That(versionList, Is.Not.Null);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(versionList.Latest.Release, Is.Not.Empty);
            Assert.That(versionList.Latest.Snapshot, Is.Not.Empty);
            Assert.That(versionList.Versions, Is.Not.Empty);

            var version = versionList.Versions[0];
            Assert.That(version.Id, Is.Not.Empty);
            Assert.That(version.Type, Is.Not.Empty);
            Assert.That(version.Url, Is.Not.Empty);
            Assert.That(version.Time, Is.Not.Empty);
            Assert.That(version.ReleaseTime, Is.Not.Empty);
            Assert.That(version.Sha1, Is.Not.Empty);
            Assert.That(version.ComplianceLevel, Is.Zero.Or.EqualTo(1));
        }
    }
}
