using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using LuckyFish.MusicPlayer.Models;
using LuckyFish.MusicPlayer.ViewModels;

namespace LuckyFish.MusicPlayer.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /// <summary>
    /// open the file
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OpenClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var a = new OpenFileDialog();
            var result = await a.ShowAsync(this);
            if (result?[0] is not null or "")
                if (DataContext is MainWindowViewModel data)
                    data.PlayChange(result[0]);
        }
        catch (Exception)
        {
            
        }
    }

    /// <summary>
    /// change the current location
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MusicTapped(object? sender, RoutedEventArgs e)
    {
        (DataContext as MainWindowViewModel)!.Player.Position = (float)(sender as Slider)!.Value;
    }

    /// <summary>
    /// Selection the media
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SelectionTapped(object? sender, RoutedEventArgs e)
    {
        var model = (sender as Grid).DataContext as MusicModel;
        (DataContext as MainWindowViewModel).PlayChange(model.Url);
    }

    /// <summary>
    /// show the song detail info
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DetailTapped(object? sender, RoutedEventArgs e)
    {
        
    }


    #region Login

    private async void NetEaseLogin(object? sender, RoutedEventArgs e)
    {
        var data = new LoginViewModel("NetEaseLogin");
        var window = new LoginView(data);
        await window.ShowDialog(this);
        (DataContext as MainWindowViewModel).Login(data.LoginInfo);
    }

    private async void QQMusicLogin(object? sender, RoutedEventArgs e)
    {
        var data = new LoginViewModel("QQMusicLogin");
        var window = new LoginView(data);
        await window.ShowDialog(this);
    }

    #endregion

    /// <summary>
    /// change playlist
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void PlaylistTapped(object? sender, RoutedEventArgs e)
    {
        if ((sender as Control)!.DataContext is not PlaylistModel data)return;
        (DataContext as MainWindowViewModel)!.PlayChange(data.Url);
    }

    /// <summary>
    /// exit the app
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ExitClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ShowContext(object? sender, KeyEventArgs e)
    {
        
    }
}