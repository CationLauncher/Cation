using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Cation.Core.Serialization;
using Cation.Models;

namespace Cation.Core.GameInstaller;

public static class MinecraftGameDownloader
{
    private const string VersionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest_v2.json";

    private static MinecraftVersionManifest.VersionManifest? _versionListCache;

    public static async Task<MinecraftVersionManifest.VersionManifest?> GetVersionListAsync()
    {
        if (_versionListCache is not null)
            return _versionListCache;

        using var httpClient = new HttpClient();
        await using var stream = await httpClient.GetStreamAsync(VersionManifestUrl);
        _versionListCache =
            await JsonSerializer.DeserializeAsync<MinecraftVersionManifest.VersionManifest>(stream,
                JsonContext.Default.VersionManifest);
        return _versionListCache;
    }

    public static async Task<MinecraftVersionManifest.VersionEntry?> GetLatestVersionAsync()
    {
        var versionList = await GetVersionListAsync();
        var latestVersionId = versionList?.Latest.Release;
        return versionList?.Versions.Find(v => v.Id == latestVersionId);
    }
}
