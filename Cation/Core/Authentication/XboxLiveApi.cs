using Cation.Models.Authentication;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class XboxLiveApi
{
    private const string UserBaseUrl = "https://user.auth.xboxlive.com/user";
    private const string XstsBaseUrl = "https://xsts.auth.xboxlive.com/xsts";

    public static async Task<UserAuthenticateResponse?> UserAuthenticateAsync(string accessToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MicrosoftClient");
        UserAuthenticateRequest request = new()
        {
            Properties = new UserAuthenticateRequestPropertiesInfo
            {
                AuthMethod = "RPS",
                SiteName = "user.auth.xboxlive.com",
                RpsTicket = $"d={accessToken}"
            },
            RelyingParty = "http://auth.xboxlive.com",
            TokenType = "JWT"
        };
        using var responseMessage = await httpClient.PostAsJsonAsync($"{UserBaseUrl}/authenticate", request,
            AuthMsJsonContext.Default.UserAuthenticateRequest);
        await using var stream = await responseMessage.Content.ReadAsStreamAsync();
        return await AuthMsJsonContext.DeserializeAsync<UserAuthenticateResponse>(stream);
    }

    public static async Task<XstsAuthorizeResponse?> XstsAuthorizeTokenAsync(string xblToken)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("MicrosoftClient");
        XstsAuthorizeRequest request = new()
        {
            Properties = new XstsAuthorizeRequestPropertiesInfo
            {
                SandboxId = "RETAIL",
                UserTokens = [xblToken]
            },
            RelyingParty = "rp://api.minecraftservices.com/",
            TokenType = "JWT"
        };
        using var responseMessage = await httpClient.PostAsJsonAsync($"{XstsBaseUrl}/authorize", request,
            AuthMsJsonContext.Default.XstsAuthorizeRequest);
        await using var stream = await responseMessage.Content.ReadAsStreamAsync();
        return await AuthMsJsonContext.DeserializeAsync<XstsAuthorizeResponse>(stream);
    }
}
