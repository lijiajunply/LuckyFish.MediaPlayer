using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Media;
using LibVLCSharp.Shared;
using LuckyFish.MusicPlayer.Models;
using LuckyFish.MusicPlayer.Server;

namespace LuckyFish.MusicPlayer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Account

    public string UserId { get; set; }

    public void Login(string softName)
    {
        MusicApi api = softName == "NetEaseMusic" ? NetServer.NetEaseUrl : NetServer.QQMusicUrl;
        NetServer.GetData(api.Url + api.UserInfo);
    }

    #endregion
    #region Musics

    private ObservableCollection<MusicModel> _playlist = new ();
    public ObservableCollection<MusicModel> Playlist
    {
        get => _playlist;
        set => SetField(ref _playlist, value);
    }

    private string? _dirPath;
    private string? DirPath
    {
        get => _dirPath;
        set => SetField(ref _dirPath, value);
    }

    private ObservableCollection<MusicModel> _musicUrl = new ();
    public ObservableCollection<MusicModel> MusicUrl
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

    private string _url;

    public string Url
    {
        get => _url;
        set
        {
            DirChanged(value);
            bool isInit = false;
            if (Player != null)
            {
                Player.Stop();
                isInit = true;
            }
            using var libvlc = new LibVLC(enableDebugLogs: true);
            using var media = new Media(libvlc, new Uri(value));
            Player = new MediaPlayer(media);
            SetField(ref _url, Path.GetFileNameWithoutExtension(value));
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
    

    public void PlayChange(string url) => Url = url;

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

    private void DirChanged(string url)
    {
        DirPath = Path.GetDirectoryName(url);
        if (DirPath == null)
        {
            
        }
        else
        {
            var dir = new DirectoryInfo(DirPath);
            MusicUrl = new ObservableCollection<MusicModel>(dir.GetFiles().Where(x => x.Extension is ".mp3" or ".flac").Select(x => new MusicModel(x.FullName))) ;
        }
        
    }
}