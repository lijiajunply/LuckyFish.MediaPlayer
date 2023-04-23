using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LuckyFish.MusicPlayer.Views;

public partial class TextView : Window
{
    public TextView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}