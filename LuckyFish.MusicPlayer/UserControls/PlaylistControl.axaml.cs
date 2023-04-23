using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace LuckyFish.MusicPlayer.UserControls;

public partial class PlaylistControl : UserControl
{
    public PlaylistControl()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}