using System;
using LibVLCSharp.Shared;

namespace LuckyFish.MusicPlayer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private MediaPlayer _player;
    public MediaPlayer Player
    {
        get => _player;
        set => SetField(ref _player, value);
    }

    public void PlayChange(string url)
    {
        using var libvlc = new LibVLC(enableDebugLogs: true);
        using var media = new Media(libvlc, new Uri(url));
        Player = new MediaPlayer(media);
    }

    public MainWindowViewModel()
    {
        PlayChange(@"C:\Users\李嘉俊\Downloads\米店.mp3");
    }

    public void Play() => Player.Play();
}