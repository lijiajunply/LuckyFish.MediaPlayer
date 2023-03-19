using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Media;
using LibVLCSharp.Shared;
using LuckyFish.MusicPlayer.Server;

namespace LuckyFish.MusicPlayer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Musics

    private string _dirPath;
    public string DirPath
    {
        get => _dirPath;
        set => SetField(ref _dirPath, value);
    }

    private ObservableCollection<string> _musicUrl = new ObservableCollection<string>();
    public ObservableCollection<string> MusicUrl
    {
        get => _musicUrl;
        set => SetField(ref _musicUrl, value);
    }

    #endregion
    private bool IsPlay { get; set; }
    
    #region Playing

    private float _pos;

    public float Pos
    {
        get => _pos;
        set => SetField(ref _pos, value);
    }

    private IImage _playImage;

    public IImage PlayImage
    {
        get => _playImage;
        set => SetField(ref _playImage, value);
    }

    private string _fileName;

    public string FileName
    {
        get => _fileName;
        set
        {
            bool isInit = false;
            if (Player != null)
            {
                Player.Stop();
                isInit = true;
            }
            using var libvlc = new LibVLC(enableDebugLogs: true);
            using var media = new Media(libvlc, new Uri(value));
            Player = new MediaPlayer(media);
            SetField(ref _fileName, Path.GetFileNameWithoutExtension(value));
            Player.PositionChanged += (sender, e) => Pos = e.Position;
            if (!isInit) return;
            IsPlay = false;
            Play();
        }
    }

    private MediaPlayer _player;

    public MediaPlayer Player
    {
        get => _player;
        set => SetField(ref _player, value);
    }
    
    #endregion
    

    public void PlayChange(string url) => FileName = url;

    public MainWindowViewModel()
    {
        PlayImage = ImageServer.PauseImage;
        PlayChange(ImageServer.CodePath + "\\Assets\\杨振宇 - 我们还是做朋友吧.flac");
    }

    public void Play()
    {
        if (IsPlay)
        {
            Player.Pause();
            PlayImage = ImageServer.PauseImage;
        }
        else
        {
            Player.Play();
            PlayImage = ImageServer.PlayImage;
        }

        IsPlay = !IsPlay;
    }

    public void DirChanged(string filename)
    {
        DirPath = Path.GetDirectoryName(filename);
        var dir = new DirectoryInfo(DirPath);
        MusicUrl = new ObservableCollection<string>(dir.GetFiles().Where(x => x.Extension == "").Select(x => x.FullName)) ;
    }
}