using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using LuckyFish.MusicPlayer.Server;
using LuckyFish.MusicPlayer.ViewModels;

namespace LuckyFish.MusicPlayer.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
    }

    public LoginView(LoginViewModel model)
    {
        InitializeComponent();
        DataContext = model;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private bool IsOK()
    {
        MusicApi api = (DataContext as LoginViewModel).LoginInfo == "NetEaseLogin" ? NetServer.NetEaseUrl : NetServer.QQMusicUrl;
        var data = NetServer.GetData(api.Url+api.LoginCheck+"?key="+(DataContext as LoginViewModel).Key);
        return data["code"].ToString() == "803";
    }

    private void ReturnClick(object? sender, RoutedEventArgs e)
    {
        if (IsOK())
            Close();
    }
}