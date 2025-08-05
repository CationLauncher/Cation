using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class XboxLiveApi
{
    public static async Task<XboxLiveToken?> GetXblTokenAsync(string accessToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MicrosoftClient");
        var request = $$"""
                        {
                            "Properties": {
                                "AuthMethod": "RPS",
                                "SiteName": "user.auth.xboxlive.com",
                                "RpsTicket": "d={{accessToken}}"
                            },
                            "RelyingParty": "http://auth.xboxlive.com",
                            "TokenType": "JWT"
                        }
                        """;
        var httpContent = new StringContent(request);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using var response =
            await httpClient.PostAsync("https://user.auth.xboxlive.com/user/authenticate", httpContent);
        JsonObject? json;
        try
        {
            json = await JsonSerializer.DeserializeAsync<JsonObject>(await response.Content.ReadAsStreamAsync());
        }
        catch (JsonException)
        {
            return null;
        }
        if (json == null)
            return null;
        var token = json["Token"]?.GetValue<string>();
        var userHash = json["DisplayClaims"]?["xui"]?[0]?["uhs"]?.GetValue<string>();
        if (token == null || userHash == null)
            return null;
        return new XboxLiveToken(userHash, token);
    }

    public static async Task<XboxLiveToken?> GetXstsTokenAsync(string xblToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MicrosoftClient");
        var request = $$"""
                        {
                            "Properties": {
                                "SandboxId": "RETAIL",
                                "UserTokens": [
                                    "{{xblToken}}"
                                ]
                            },
                            "RelyingParty": "rp://api.minecraftservices.com/",
                            "TokenType": "JWT"
                        }
                        """;
        var httpContent = new StringContent(request);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using var response =
            await httpClient.PostAsync("https://xsts.auth.xboxlive.com/xsts/authorize", httpContent);
        JsonObject? json;
        try
        {
            json = await JsonSerializer.DeserializeAsync<JsonObject>(await response.Content.ReadAsStreamAsync());
        }
        catch (JsonException)
        {
            return null;
        }
        if (json == null)
            return null;
        var token = json["Token"]?.GetValue<string>();
        var userHash = json["DisplayClaims"]?["xui"]?[0]?["uhs"]?.GetValue<string>();
        if (token == null || userHash == null)
            return null;
        return new XboxLiveToken(userHash, token);
    }
}
