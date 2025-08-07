using System.Text.Json.Serialization;

namespace Cation.Models.Authentication;

[JsonSourceGenerationOptions(
    WriteIndented = true
)]
[JsonSerializable(typeof(UserAuthenticateRequest))]
[JsonSerializable(typeof(UserAuthenticateResponse))]
[JsonSerializable(typeof(XstsAuthorizeRequest))]
[JsonSerializable(typeof(XstsAuthorizeResponse))]
public partial class AuthMsJsonContext : JsonSerializerContext
{
}

[JsonSourceGenerationOptions(
    WriteIndented = true,
    PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower
)]
[JsonSerializable(typeof(AuthenticationLoginWithXboxRequest))]
[JsonSerializable(typeof(AuthenticationLoginWithXboxResponse))]
[JsonSerializable(typeof(MinecraftProfileResponse))]
public partial class AuthMcJsonContext : JsonSerializerContext
{
}
