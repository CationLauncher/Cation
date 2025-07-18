using Cation.Models;
using System.Text.Json.Serialization;

namespace Cation.Core.Serialization;

[JsonSerializable(typeof(MinecraftVersionManifest.VersionManifest))]
[JsonSerializable(typeof(MinecraftVersionManifest.LatestInfo))]
[JsonSerializable(typeof(MinecraftVersionManifest.VersionEntry))]
public partial class JsonContext : JsonSerializerContext
{
}
