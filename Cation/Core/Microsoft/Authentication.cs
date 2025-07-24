using Microsoft.Identity.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cation.Core.Microsoft;

public static class Authentication
{
    private static readonly string[] Scopes = ["user.read"];

    public static async Task<AuthenticationResult?> GetAccessTokenAsync()
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
                var result = await pca.AcquireTokenWithDeviceCode(Scopes,
                    deviceCodeResult =>
                    {
                        Console.WriteLine(deviceCodeResult.Message);
                        return Task.FromResult(0);
                    }).ExecuteAsync().ConfigureAwait(false);

                Console.WriteLine(result.Account.Username);
                return result;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return null;
    }
}
