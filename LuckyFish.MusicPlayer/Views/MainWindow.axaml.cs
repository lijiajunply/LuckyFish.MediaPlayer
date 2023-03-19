using Avalonia.Controls;
using Avalonia.Interactivity;
using LuckyFish.MusicPlayer.ViewModels;

namespace LuckyFish.MusicPlayer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void OpenClick(object? sender, RoutedEventArgs e)
    {
        var a = new OpenFileDialog();
        var result = await a.ShowAsync(this);
        if (result[0] is not null or "")
        {
            var data = DataContext as MainWindowViewModel;
            data.PlayChange(result[0]);
        }
    }

    private void MusicTapped(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel).Player.Position = (float)(sender as Slider).Value;
    }

    private async void NetEaseLogin(object? sender, RoutedEventArgs e)
    {
        var data = new LoginViewModel("NetEaseLogin");
        var window = new LoginView(data);
        await window.ShowDialog(this);
    }

    private void QQMusicLogin(object? sender, RoutedEventArgs e)
    {
        
    }
}