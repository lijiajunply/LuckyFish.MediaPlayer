using System.IO;

namespace LuckyFish.MusicPlayer.Models;

public class MusicModel
{
    public MusicModel(string name, string url, int id)
    {
        Name = name;
        Url = url;
        Id = id;
    }

    public MusicModel(string name, string url)
    {
        Name = name;
        Url = url;
        Id = 1;
    }

    public MusicModel(string url)
    {
        Url = url;
        Name = File.Exists(url) ? Path.GetFileNameWithoutExtension(url) : url;
        Id = 1;
    }

    public string Name { get; set; }
    public string Url { get; set; }
    public int Id { get; set; }
}