using System.Text.Json.Serialization;

namespace Cation.Models.Minecraft;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
)]
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
public partial class MinecraftJsonContext : JsonSerializerContext
{
}
