using Avalonia.Controls;

namespace Cation.Controls;

public class CationUserControl<T> : UserControl where T : new()
{
    // ReSharper disable once MemberCanBeProtected.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public readonly T ViewModel = new();

    protected CationUserControl()
    {
        DataContext = ViewModel;
    }
}
