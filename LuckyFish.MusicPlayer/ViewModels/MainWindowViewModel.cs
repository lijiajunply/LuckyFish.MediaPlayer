using System;
using LibVLCSharp.Shared;

namespace LuckyFish.MusicPlayer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _fileName;
    public string FileName
    {
        get => _fileName;
        set
        {
            SetField(ref _fileName, value);
            using var libvlc = new LibVLC(enableDebugLogs: true);
            using var media = new Media(libvlc, new Uri(FileName));
            Player = new MediaPlayer(media);
        } 
    }
    private bool IsPlay { get; set; }
    private MediaPlayer _player;
    public MediaPlayer Player
    {
        get => _player;
        set => SetField(ref _player, value);
    }

    public void PlayChange(string url)
    {
        FileName = url;
    }

    public MainWindowViewModel()
    {
        PlayChange( @"C:\Users\李嘉俊\Downloads\米店.mp3");
    }

    public void Play()
    {
        
        if (IsPlay)
            Player.Pause();
        else
            Player.Play();

        IsPlay = !IsPlay;
    }
}