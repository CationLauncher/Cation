using Cation.Core.Authentication;
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
    private JavaVersion? _selectedJavaVersion;

    [ObservableProperty]
    private List<string> _gameInstances;

    [ObservableProperty]
    private string? _selectedGameInstance;

    [ObservableProperty]
    private string _username = "shatyuka";

    [ObservableProperty]
    private UserTypeItem _selectedUserType;

    public List<UserTypeItem> UserTypes { get; } =
    [
        new() { Value = "msa", Display = "Microsoft" },
        new() { Value = "legacy", Display = "Offline" }
    ];

    public class UserTypeItem
    {
        public required string Value { get; init; }
        public required string Display { get; init; }
        public bool IsMicrosoftUserType => Value == "msa";
        public bool IsOfflineUserType => Value == "legacy";
    }

    [ObservableProperty]
    private string _msCode = "";

    [ObservableProperty]
    private MinecraftProfile? _minecraftProfile;
}
