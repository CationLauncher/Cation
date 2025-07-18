using Avalonia.Headless.NUnit;
using Cation.ViewModels;
using Cation.Views;

namespace Cation.Test.UI;

public class MainWindowTest
{
    private MainWindow _mainWindow;
    private MainWindowViewModel _viewModel;
    
    [SetUp]
    public void Setup()
    {
        _viewModel = new MainWindowViewModel();
        _mainWindow = new MainWindow
        {
            DataContext = _viewModel
        };
    }

    [AvaloniaTest]
    public void MainWindow()
    {
        Assert.That(_mainWindow, Is.Not.Null);
    }
}
