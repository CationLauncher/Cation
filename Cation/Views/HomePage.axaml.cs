using Avalonia.Interactivity;
using Cation.Controls;
using Cation.Core.Java;
using Cation.Core.Minecraft;
using Cation.ViewModels;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
        if (ViewModel.SelectedJavaVersionIndex < 0 ||
            ViewModel.SelectedJavaVersionIndex >= ViewModel.JavaVersions.Count)
        {
            Console.WriteLine("No Java selected.");
            return;
        }

        var javaVersion = ViewModel.JavaVersions[ViewModel.SelectedJavaVersionIndex];

        if (ViewModel.SelectedGameInstanceIndex < 0 ||
            ViewModel.SelectedGameInstanceIndex >= ViewModel.GameInstances.Count)
        {
            Console.WriteLine("No game selected.");
            return;
        }

        var gameInstance = ViewModel.GameInstances[ViewModel.SelectedGameInstanceIndex];

        var javaExe = Path.Combine(javaVersion.Path, JavaManager.GameExecutableName);
        var args = GameManager.GetGameArguments(gameInstance, ViewModel.Username);
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
}
