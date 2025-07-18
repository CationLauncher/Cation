using Avalonia;
using Avalonia.Headless;
using Avalonia.Headless.NUnit;
using Cation.Test.UI;

[assembly: AvaloniaTestApplication(typeof(AppTest))]

namespace Cation.Test.UI;

public class AppTest
{
    private static AppBuilder? _builder;

    private static AppBuilder BuildAvaloniaApp()
    {
        _builder = Program.BuildAvaloniaApp().UseHeadless(new AvaloniaHeadlessPlatformOptions());
        return _builder;
    }

    [AvaloniaTest]
    public void App()
    {
        Assert.That(_builder, Is.Not.Null);
        Assert.That(_builder.Instance, Is.Not.Null);
        Assert.That(_builder.Instance, Is.InstanceOf<App>());
    }
}
