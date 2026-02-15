using System.Text.Json.Serialization;

namespace Cation.Models.ModPlatform.CurseForge;

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase
)]
[JsonSerializable(typeof(GetCategoriesResponse))]
[JsonSerializable(typeof(SearchModsResponse))]
public partial class CurseForgeJsonContext : JsonSerializerContext
{
}
