using Avalonia.Controls;

namespace Cation.Controls;

public class CationWindow<T> : Window where T : new()
{
    // ReSharper disable once MemberCanBeProtected.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly T ViewModel = new();

    protected CationWindow()
    {
        DataContext = ViewModel;
    }
}
