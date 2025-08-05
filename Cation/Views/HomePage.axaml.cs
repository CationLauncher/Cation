using Avalonia.Controls;
using Avalonia.Interactivity;
using Cation.Controls;
using Cation.Core.Authentication;
using Cation.Core.Java;
using Cation.Core.Minecraft;
using Cation.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cation.Views;

public partial class HomePage : CationUserControl<HomePageViewModel>
{
    public HomePage()
    {
        InitializeComponent();

        ViewModel.JavaVersions = JavaManager.GetJavaList();
        if (ViewModel.JavaVersions.Count > 0)
            ViewModel.SelectedJavaVersion = ViewModel.JavaVersions[0];
        ViewModel.GameInstances = GameManager.GetGameInstances();
        if (ViewModel.GameInstances.Count > 0)
            ViewModel.SelectedGameInstance = ViewModel.GameInstances[0];
    }

    private void StartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var javaVersion = ViewModel.SelectedJavaVersion;
        if (javaVersion == null)
        {
            Console.WriteLine("No Java version selected.");
            return;
        }

        var gameInstance = ViewModel.SelectedGameInstance;
        if (gameInstance == null)
        {
            Console.WriteLine("No game instance selected.");
            return;
        }

        var userType = ViewModel.SelectedUserType.Value;
        if (userType == "msa" && ViewModel.MinecraftProfile == null)
        {
            Console.WriteLine("No Microsoft account profile found. Please log in first.");
            return;
        }

        var username = userType == "msa" ? ViewModel.MinecraftProfile!.Username : ViewModel.Username;
        var userId = userType == "msa" ? ViewModel.MinecraftProfile!.Id : "";
        var accessToken = userType == "msa" ? ViewModel.MinecraftProfile!.AccessToken : "";

        var javaExe = Path.Combine(javaVersion.Path, JavaManager.GameExecutableName);
        var args = GameManager.GetGameArguments(gameInstance, username, userType, userId, accessToken);
        if (args == null)
        {
            Console.WriteLine("Failed to get game arguments.");
            return;
        }

        var minecraftPath = Path.GetDirectoryName(Path.GetDirectoryName(gameInstance))!;

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = javaExe,
                Arguments = args,
                WorkingDirectory = minecraftPath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8
            }
        };

        process.Start();
    }

    private async void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var result = await Authentication.AuthenticateWithMsaAsync(deviceCodeResult =>
            {
                ViewModel.MsCode = deviceCodeResult.UserCode;
                var launcher = TopLevel.GetTopLevel(this)?.Launcher;
                launcher?.LaunchUriAsync(new Uri(deviceCodeResult.VerificationUrl));
                return Task.FromResult(0);
            });
            if (result == null)
                return;

            ViewModel.MsCode = "";
            ViewModel.MinecraftProfile = result;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}
