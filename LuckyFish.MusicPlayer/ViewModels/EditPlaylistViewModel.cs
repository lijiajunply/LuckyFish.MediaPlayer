using System.Collections.Generic;
using System.Collections.ObjectModel;
using LuckyFish.MusicPlayer.Models;

namespace LuckyFish.MusicPlayer.ViewModels;

public class EditPlaylistViewModel : ViewModelBase
{
    private ObservableCollection<PlaylistModel> _models = new();
    public ObservableCollection<PlaylistModel> Models
    {
        get => _models;
        set => SetField(ref _models, value);
    }
    public EditPlaylistViewModel(){}

    public EditPlaylistViewModel(PlaylistModel model) =>
        Models = new ObservableCollection<PlaylistModel>(new[] { model });
}