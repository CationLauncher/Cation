using Cation.Core.Java;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace Cation.ViewModels;

[ObservableRecipient]
public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    private List<JavaVersion> _javaVersions;

    [ObservableProperty]
    private int _selectedJavaVersionIndex;

    [ObservableProperty]
    private List<string> _gameInstances;

    [ObservableProperty]
    private int _selectedGameInstanceIndex;

    [ObservableProperty]
    private string _username = "shatyuka";
}
