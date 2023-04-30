using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace LuckyFish.MusicPlayer.Views;

public partial class EditPlaylistView : Window
{
    public EditPlaylistView()
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

    private void AddMusicClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void DeleteClick(object? sender, RoutedEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}