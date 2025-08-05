using Microsoft.Identity.Client;
using System;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class Authentication
{
    public static async Task<MinecraftProfile?> AuthenticateWithMsaAsync(
        Func<DeviceCodeResult, Task> deviceCodeCallback)
    {
        var authenticationResult = await MicrosoftAuthentication.GetAccessTokenAsync(deviceCodeCallback);
        if (authenticationResult == null)
            return null;
        var xblToken = await XboxLiveApi.GetXblTokenAsync(authenticationResult.AccessToken);
        if (xblToken == null)
            return null;
        var xstsToken = await XboxLiveApi.GetXstsTokenAsync(xblToken.Token);
        if (xstsToken == null)
            return null;
        var minecraftToken = await MinecraftApi.LoginWithXboxAsync(xstsToken);
        if (minecraftToken == null)
            return null;
        return await MinecraftApi.GetProfileAsync(minecraftToken);
    }
}
