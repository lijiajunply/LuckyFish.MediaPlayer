using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using Avalonia.Media;
using LibVLCSharp.Shared;
using LuckyFish.MusicPlayer.Models;
using LuckyFish.MusicPlayer.Server;

namespace LuckyFish.MusicPlayer.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    #region Account

    /// <summary>
    /// the user id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// login func
    /// </summary>
    /// <param name="softName">NetEaseMusic or other</param>
    public void Login(string softName)
    {
        MusicApi api = softName == "NetEaseMusic" ? NetServer.NetEaseUrl : NetServer.QQMusicUrl;
        NetServer.GetData(api.Url + api.UserInfo);
    }

    #endregion

    #region Musics

    private ObservableCollection<PlaylistModel> _playlists = new();

    /// <summary>
    /// playlists
    /// </summary>
    public ObservableCollection<PlaylistModel> Playlists
    {
        get => _playlists;
        set => SetField(ref _playlists, value);
    }

    private string? _dirPath;

    /// <summary>
    /// the music pos
    /// </summary>
    private string? DirPath
    {
        get => _dirPath;
        set => SetField(ref _dirPath, value);
    }

    private ObservableCollection<MusicModel> _playlist = new();

    /// <summary>
    /// the playlist
    /// </summary>
    public ObservableCollection<MusicModel> Playlist
    {
        get => _playlist;
        set => SetField(ref _playlist, value);
    }
    
    /// <summary>
    /// the pos of the music in the playlist
    /// </summary>
    private int Index { get; set; }
    
    #endregion

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

            using var vlc = new LibVLC(enableDebugLogs: true);
            using var media = new Media(vlc, new Uri(value));
            Player = new MediaPlayer(media);

            SetField(ref _url, Path.GetFileNameWithoutExtension(value));

            Player.PositionChanged += PositionChange;
            Player.EndReached += Done;

            if (!isInit) return;
            IsPlaying = false;
            Play();
        }
    }

    private bool IsPlaying { get; set; }

    #endregion

    #region PlayerSetting

    #region PlayerEvents

    private void PositionChange(object? sender, MediaPlayerPositionChangedEventArgs e)
    {
        Pos = e.Position;
    }

    private void Done(object? sender, EventArgs e)
    {
        switch (Mode)
        {
            case "Single" or "":
                return;
            case "Single Loop":
                break;
            case "List":
                Index++;
                if (Index >= Playlist.Count)
                    Index = 0;
                break;
            case "Random":
                Random random = new Random();
                int ran = random.Next(0, Playlist.Count - 1);
                Index = ran == Index?Index+1:ran;
                if (Index >= Playlist.Count || Index <= 0)
                    Index = 0;
                break;
        }

        var s = Playlist[Index].Url;
        ThreadPool.QueueUserWorkItem(_ => Url = s);
    }

    #endregion
    
    private string _mode = "Single";

    /// <summary>
    /// Mode : Single Loop| List | Random | Single
    /// </summary>
    public string Mode
    {
        get => _mode;
        set => SetField(ref _mode, value);
    }

    /// <summary>
    /// Mode change : Single -> Single Loop -> List -> Random -> Single
    /// </summary>
    public void ModeChange()
    {
        if (Mode == "Single")
        {
            Mode = "Single Loop";
            PlayModeImage = ImageServer.SingleLoop;
            return;
        }

        if (Mode == "Single Loop")
        {
            Mode = "List";
            PlayModeImage = ImageServer.ListLoop;
            return;
        }

        if (Mode == "List")
        {
            Mode = "Random";
            PlayModeImage = ImageServer.RandomLoop;
            return;
        }

        if (Mode == "Random")
        {
            Mode = "Single";
            PlayModeImage = ImageServer.Single;
        }
    }

    private IImage _playModeImage = ImageServer.Single;

    public IImage PlayModeImage
    {
        get => _playModeImage;
        set => SetField(ref _playModeImage, value);
    }

    private MediaPlayer? _player;

    public MediaPlayer? Player
    {
        get => _player;
        set => SetField(ref _player, value);
    }

    #endregion

    #region Func

    public void PlayChange(string url) => Url = url;

    public void Play()
    {
        if (Player == null) return;
        if (IsPlaying)
        {
            Player.Pause();
            PlayImage = ImageServer.PauseImage;
        }
        else
        {
            Player.Play();
            PlayImage = ImageServer.PlayImage;
        }

        IsPlaying = !IsPlaying;
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
            Playlist = new ObservableCollection<MusicModel>(dir.GetFiles().Where(x => x.Extension is ".mp3" or ".flac")
                .Select(x => new MusicModel(x.FullName)));
            var model = Playlist.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x.Url) == url);
            if (model != null) Index = Playlist.IndexOf(model);
        }
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public MainWindowViewModel()
    {
        PlayImage = ImageServer.PauseImage;
        PlayChange(ImageServer.CodePath + "\\Assets\\杨振宇 - 我们还是做朋友吧.flac");
    }
}