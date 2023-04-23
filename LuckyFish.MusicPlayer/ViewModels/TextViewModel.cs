namespace LuckyFish.MusicPlayer.ViewModels;

public class TextViewModel : ViewModelBase
{
    private string _context = "";
    public string Context
    {
        get => _context;
        set => SetField(ref _context, value);
    }
    public TextViewModel(){}
    public TextViewModel(string context) => Context = context;

}