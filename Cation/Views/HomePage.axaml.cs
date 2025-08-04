using Avalonia.Controls;
using Avalonia.Interactivity;
using Cation.Controls;
using Cation.Core.Java;
using Cation.Core.Microsoft;
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
        ViewModel.GameInstances = GameManager.GetGameInstances();
    }

    private void StartButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var javaVersion = ViewModel.SelectedJavaVersion;
        var gameInstance = ViewModel.SelectedGameInstance;
        var userType = ViewModel.SelectedUserType.Value;

        var javaExe = Path.Combine(javaVersion.Path, JavaManager.GameExecutableName);
        var args = GameManager.GetGameArguments(gameInstance, ViewModel.Username, userType);
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
        Console.WriteLine(process.StandardOutput.ReadToEnd());
        Console.WriteLine(process.StandardError.ReadToEnd());
    }

    private async void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var result = await Authentication.GetMicrosoftAccessTokenAsync(deviceCodeResult =>
            {
                ViewModel.MsCode = deviceCodeResult.UserCode;
                var launcher = TopLevel.GetTopLevel(this)?.Launcher;
                launcher?.LaunchUriAsync(new Uri(deviceCodeResult.VerificationUrl));
                return Task.FromResult(0);
            });
            if (result == null)
                return;

            ViewModel.MsUsername = result.Account.Username;
            ViewModel.AccessToken = result.AccessToken;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}
