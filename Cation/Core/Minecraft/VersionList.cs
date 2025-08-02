using Cation.Models.Minecraft;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Cation.Core.Minecraft;

public static class VersionList
{
    private const string VersionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest_v2.json";

    private static VersionManifest? _versionListCache;

    public static async Task<VersionManifest?> GetVersionListAsync()
    {
        if (_versionListCache is not null)
            return _versionListCache;

        using var httpClient = App.HttpClientFactory.CreateClient("MinecraftClient");
        await using var stream = await httpClient.GetStreamAsync(VersionManifestUrl);
        var manifest = await JsonContext.DeserializeAsync<VersionManifest>(stream);
        manifest?.Versions.Sort((a, b) => b.ReleaseTime.CompareTo(a.ReleaseTime));
        _versionListCache = manifest;
        return _versionListCache;
    }

    public static async Task<string?> GetLatestVersionId()
    {
        var versionList = await GetVersionListAsync();
        return versionList?.Latest.Release;
    }

    public static async Task<VersionManifest.VersionInfo?> GetVersionInfoAsync(string id)
    {
        var versionList = await GetVersionListAsync();
        return versionList?.Versions.Find(v => v.Id == id);
    }

    public static async Task<Client?> GetClientAsync(string id)
    {
        var versionInfo = await GetVersionInfoAsync(id);
        if (versionInfo is null)
            return null;

        using var httpClient = App.HttpClientFactory.CreateClient("MinecraftClient");
        var responseBytes = await httpClient.GetByteArrayAsync(versionInfo.Url);

        var hash = SHA1.HashData(responseBytes);
        var hashString = Convert.ToHexString(hash).ToLowerInvariant();
        if (!hashString.Equals(versionInfo.Sha1, StringComparison.InvariantCultureIgnoreCase))
        {
            Console.WriteLine($"SHA1 validation failed. Expected: {versionInfo.Sha1}, actual: {hashString}");
            return null;
        }

        using var stream = new MemoryStream(responseBytes);
        return await JsonContext.DeserializeAsync<Client>(stream);
    }
}
