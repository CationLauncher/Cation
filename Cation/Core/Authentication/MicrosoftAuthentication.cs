using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class MicrosoftAuthentication
{
    private static readonly string[] Scopes = ["XboxLive.signin"];

    public static async Task<AuthenticationResult?> GetAccessTokenAsync(
        Func<DeviceCodeResult, Task> deviceCodeResultCallback)
    {
        var pca = PublicClientApplicationBuilder.Create(BuildConfig.MicrosoftClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "consumers")
            .WithDefaultRedirectUri()
            .Build();

        var accounts = await pca.GetAccountsAsync();

        try
        {
            return await pca.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync();
        }
        catch (Exception)
        {
            try
            {
                return await pca.AcquireTokenWithDeviceCode(Scopes, deviceCodeResultCallback).ExecuteAsync()
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return null;
    }
}
