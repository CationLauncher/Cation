using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cation.Core.Authentication;

public static class MicrosoftAuthentication
{
    private static readonly string[] Scopes = ["XboxLive.signin"];

    private static IPublicClientApplication? _pca;

    public static async Task<AuthenticationResult?> GetAccessTokenAsync(
        Func<DeviceCodeResult, Task> deviceCodeResultCallback)
    {
        if (_pca == null)
        {
            string configDir;
            if (OperatingSystem.IsMacOS() || OperatingSystem.IsWindows())
            {
                var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                configDir = Path.Combine(local, "CationLauncher");
            }
            else
            {
                var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                configDir = Path.Combine(home, ".config", "CationLauncher");
            }

            var storageProperties =
                new StorageCreationPropertiesBuilder("msa_cache", configDir)
                    .WithMacKeyChain(
                        "Cation Launcher",
                        "msa")
                    .WithLinuxKeyring(
                        "com.cationlauncher.cation",
                        "default",
                        "Credentials used by Cation Launcher",
                        default,
                        default)
                    .Build();

            _pca = PublicClientApplicationBuilder.Create(BuildConfig.MicrosoftClientId)
                .WithAuthority(AzureCloudInstance.AzurePublic, "consumers")
                .WithDefaultRedirectUri()
                .Build();

            var cacheHelper = await MsalCacheHelper.CreateAsync(storageProperties);
            cacheHelper.RegisterCache(_pca.UserTokenCache);
        }

        var accounts = await _pca.GetAccountsAsync();

        try
        {
            return await _pca.AcquireTokenSilent(Scopes, accounts.FirstOrDefault()).ExecuteAsync();
        }
        catch (Exception)
        {
            try
            {
                return await _pca.AcquireTokenWithDeviceCode(Scopes, deviceCodeResultCallback).ExecuteAsync()
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
