using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Cation.Core.Network;
using Cation.ViewModels;
using Cation.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;

namespace Cation;

public class App : Application
{
    private static IServiceProvider Services { get; set; } = null!;
    public static IHttpClientFactory HttpClientFactory => Services.GetRequiredService<IHttpClientFactory>();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    internal static void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddHttpClient("MinecraftClient", client => client.SetupHttpClient());
        services.AddHttpClient("ForgeClient", client => client.SetupHttpClient());
        Services = services.BuildServiceProvider();
    }

    [ExcludeFromCodeCoverage]
    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureServices();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit.
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    [ExcludeFromCodeCoverage]
    private static void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}
