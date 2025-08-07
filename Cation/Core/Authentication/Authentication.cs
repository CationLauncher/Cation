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
        var userAuthenticate = await XboxLiveApi.UserAuthenticateAsync(authenticationResult.AccessToken);
        if (userAuthenticate == null)
            return null;
        var xstsAuthorize = await XboxLiveApi.XstsAuthorizeTokenAsync(userAuthenticate.Token);
        if (xstsAuthorize == null)
            return null;
        if (xstsAuthorize.DisplayClaims.Xui.Count == 0)
            return null;
        var uhs = xstsAuthorize.DisplayClaims.Xui[0].Uhs;
        var minecraftToken = await MinecraftApi.LoginWithXboxAsync(uhs, xstsAuthorize.Token);
        if (minecraftToken == null)
            return null;
        var profile = await MinecraftApi.GetProfileAsync(minecraftToken.AccessToken);
        if (profile == null)
            return null;
        return new MinecraftProfile(profile.Id, profile.Name, minecraftToken.AccessToken);
    }
}
