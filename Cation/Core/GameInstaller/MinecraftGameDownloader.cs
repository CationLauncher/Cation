using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Cation.Core.Serialization;
using Cation.Models;

namespace Cation.Core.GameInstaller;

public static class MinecraftGameDownloader
{
    private const string VersionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest_v2.json";

    public static async Task<MinecraftVersionManifest.VersionManifest?> GetVersionListAsync()
    {
        using var httpClient = new HttpClient();
        await using var stream = await httpClient.GetStreamAsync(VersionManifestUrl);
        return await JsonSerializer.DeserializeAsync<MinecraftVersionManifest.VersionManifest>(stream, JsonContext.Default.VersionManifest);
    }
}
