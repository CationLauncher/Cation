using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Cation.Models.Minecraft;

[JsonSerializable(typeof(VersionManifest))]
[JsonSerializable(typeof(VersionManifest.LatestInfo))]
[JsonSerializable(typeof(VersionManifest.VersionInfo))]
[JsonSerializable(typeof(Client))]
[JsonSerializable(typeof(Client.AssetIndexInfo))]
[JsonSerializable(typeof(Client.DownloadInfo))]
[JsonSerializable(typeof(Client.DownloadsInfo))]
[JsonSerializable(typeof(Client.JavaVersionInfo))]
[JsonSerializable(typeof(Client.LibraryInfo))]
[JsonSerializable(typeof(Client.ArgumentsInfo))]
[JsonSerializable(typeof(Client.LoggingInfo))]
public partial class JsonContext : JsonSerializerContext
{
    public static async Task<TValue?> DeserializeAsync<TValue>(
        Stream utf8Json,
        CancellationToken cancellationToken = default)
    {
        return (TValue?)await JsonSerializer.DeserializeAsync(utf8Json, Default.GetTypeInfo(typeof(TValue))!,
            cancellationToken);
    }
}
