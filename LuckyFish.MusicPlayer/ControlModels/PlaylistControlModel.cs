using Avalonia.Media;
using LuckyFish.MusicPlayer.Models;
using LuckyFish.MusicPlayer.Server;
using LuckyFish.MusicPlayer.ViewModels;

namespace LuckyFish.MusicPlayer.ControlModels;

public class PlaylistControlModel : ViewModelBase
{
    #region Porp

    private PlaylistModel _model;

    public PlaylistModel Model
    {
        get => _model;
        set => SetField(ref _model, value);
    }

    private IImage _image = ImageServer.DefaultImage;

    public IImage Image
    {
        get => _image;
        set => SetField(ref _image, value);
    }

    #endregion
    
    public PlaylistControlModel(){}

    public PlaylistControlModel(PlaylistModel model)
    {
        Model = model;
        var a = Model.Musics[0].Image;
        Image = a ?? ImageServer.DefaultImage;
    }
}