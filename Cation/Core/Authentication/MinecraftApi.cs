using Cation.Models.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class MinecraftApi
{
    private const string BaseUrl = "https://api.minecraftservices.com";

    public static async Task<AuthenticationLoginWithXboxResponse?> LoginWithXboxAsync(string userHash, string xstsToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MinecraftClient");
        AuthenticationLoginWithXboxRequest request = new()
        {
            IdentityToken = $"XBL3.0 x={userHash};{xstsToken}"
        };
        using var responseMessage = await httpClient.PostAsJsonAsync($"{BaseUrl}/authentication/login_with_xbox",
            request, AuthMcJsonContext.Default.AuthenticationLoginWithXboxRequest);
        await using var stream = await responseMessage.Content.ReadAsStreamAsync();
        return await AuthMcJsonContext.DeserializeAsync<AuthenticationLoginWithXboxResponse>(stream);
    }

    public static async Task<MinecraftProfileResponse?> GetProfileAsync(string minecraftToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MinecraftClient");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", minecraftToken);
        await using var stream = await httpClient.GetStreamAsync($"{BaseUrl}/minecraft/profile");
        return await AuthMcJsonContext.DeserializeAsync<MinecraftProfileResponse>(stream);
    }
}
