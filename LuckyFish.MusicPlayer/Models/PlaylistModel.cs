using System.Collections.Generic;

namespace LuckyFish.MusicPlayer.Models;

public class PlaylistModel
{
    public PlaylistModel(string name, string url)
    {
        Name = name;
        Url = url;
    }
    public string Name { get; set; }
    public string Url { get; set; }
    public List<MusicModel> Musics { get; set; } = new ();
}