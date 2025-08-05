using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class MinecraftApi
{
    public static async Task<string?> LoginWithXboxAsync(XboxLiveToken xstsToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MinecraftClient");
        var request = $$"""
                        {
                            "identityToken": "XBL3.0 x={{xstsToken.UserHash}};{{xstsToken.Token}}"
                        }
                        """;
        var httpContent = new StringContent(request);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using var response =
            await httpClient.PostAsync("https://api.minecraftservices.com/authentication/login_with_xbox", httpContent);
        JsonObject? json;
        try
        {
            json = await JsonSerializer.DeserializeAsync<JsonObject>(await response.Content.ReadAsStreamAsync());
        }
        catch (JsonException)
        {
            return null;
        }

        return json?["access_token"]?.GetValue<string>();
    }

    public static async Task<MinecraftProfile?> GetProfileAsync(string minecraftToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MinecraftClient");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", minecraftToken);
        await using var stream = await httpClient.GetStreamAsync("https://api.minecraftservices.com/minecraft/profile");
        JsonObject? json;
        try
        {
            json = await JsonSerializer.DeserializeAsync<JsonObject>(stream);
        }
        catch (JsonException)
        {
            return null;
        }

        if (json == null)
            return null;
        var id = json["id"]?.GetValue<string>();
        var name = json["name"]?.GetValue<string>();
        if (id == null || name == null)
            return null;
        return new MinecraftProfile(id, name, minecraftToken);
    }
}
