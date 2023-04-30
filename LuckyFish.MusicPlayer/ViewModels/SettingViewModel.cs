using LuckyFish.MusicPlayer.Models;
using LuckyFish.MusicPlayer.Server;

namespace LuckyFish.MusicPlayer.ViewModels;

public class SettingViewModel : ViewModelBase
{
    private ProjectInfo? _project;

    public ProjectInfo? ProjectInfo
    {
        get => _project;
        set => SetField(ref _project, value);
    }
    public SettingViewModel()
    {
        ProjectInfo = ProjectServer.Read();
    }
}