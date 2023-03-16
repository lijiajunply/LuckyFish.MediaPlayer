using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using LibVLCSharp.Shared;
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
            (DataContext as MainWindowViewModel).PlayChange(result[0]);
            (DataContext as MainWindowViewModel).Play();
        }
    }
}