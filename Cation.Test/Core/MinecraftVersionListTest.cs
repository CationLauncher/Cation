using Cation.Core.GameInstaller;

namespace Cation.Test.Core;

public class MinecraftVersionListTest
{
    [SetUp]
    public async Task Setup()
    {
        // Cache the version list to avoid repeated network requests
        await MinecraftGameDownloader.GetVersionListAsync();
    }

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
        }
    }

    [Test]
    public async Task GetLatestVersionInfo()
    {
        var latestVersionId = await MinecraftGameDownloader.GetLatestVersionId();
        Assert.That(latestVersionId, Is.Not.Null);

        var latestVersionInfo = await MinecraftGameDownloader.GetVersionInfoAsync(latestVersionId);
        Assert.That(latestVersionInfo, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(latestVersionInfo.Id, Is.Not.Empty);
            Assert.That(latestVersionInfo.Type, Is.Not.Empty);
            Assert.That(latestVersionInfo.Url, Is.Not.Empty);
            Assert.That(latestVersionInfo.Time, Is.Not.Empty);
            Assert.That(latestVersionInfo.ReleaseTime, Is.Not.Empty);
            Assert.That(latestVersionInfo.Sha1, Is.Not.Empty);
            Assert.That(latestVersionInfo.ComplianceLevel, Is.Zero.Or.EqualTo(1));
        }
    }

    [Test]
    [Parallelizable(ParallelScope.All)]
    [TestCase("1.0")]
    [TestCase("1.6.1")]
    [TestCase("1.13")]
    [TestCase("1.17")]
    public async Task GetClient(string id)
    {
        var versionList = await MinecraftGameDownloader.GetVersionListAsync();
        Assert.That(versionList, Is.Not.Null);

        var client = await MinecraftGameDownloader.GetClientAsync(id);
        Assert.That(client, Is.Not.Null);
        using (Assert.EnterMultipleScope())
        {
            Assert.That(client.AssetIndex, Is.Not.Null);
            Assert.That(client.Assets, Is.Not.Empty);
            Assert.That(client.ComplianceLevel, Is.Zero.Or.EqualTo(1));
            Assert.That(client.Downloads, Is.Not.Null);
            Assert.That(client.Id, Is.Not.Null);
            Assert.That(client.Libraries, Is.Not.Null);
            Assert.That(client.MainClass, Is.Not.Empty);
            Assert.That(client.MinimumLauncherVersion, Is.GreaterThan(0));
            Assert.That(client.ReleaseTime, Is.Not.Empty);
            Assert.That(client.Time, Is.Not.Empty);
            Assert.That(client.Type, Is.Not.Empty);
        }

        if (Version.Parse(id) < new Version(1, 13))
        {
            Assert.That(client.MinecraftArguments, Is.Not.Empty);
        }
        else
        {
            Assert.That(client.Arguments, Is.Not.Null);
        }

        if (Version.Parse(id) >= new Version(1, 17))
        {
            Assert.That(client.JavaVersion, Is.Not.Null);
        }
    }

    [Test]
    public async Task GetClientNotFound()
    {
        var client = await MinecraftGameDownloader.GetClientAsync("0.0");
        Assert.That(client, Is.Null);
    }
}
