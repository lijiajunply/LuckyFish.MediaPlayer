using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using LuckyFish.MusicPlayer.Server;
using TagLib;
using File = System.IO.File;

namespace LuckyFish.MusicPlayer.Models;

public class MusicModel
{
    public MusicModel(string name, string url, int id = 1)
    {
        Name = name;
        Url = url;
        Id = id;
        GetImage();
    }

    public MusicModel(string url)
    {
        Url = url;
        Name = File.Exists(url) ? Path.GetFileNameWithoutExtension(url) : url;
        Id = 1;
        GetImage();
    }

    private void GetImage()
    {
        if (!File.Exists(Url))
        {
            Default();
            return;
        }
        TagLib.File currentFile = TagLib.File.Create(Url);
        IPicture[] pictures = currentFile.Tag.Pictures;
        if (pictures.Length <= 0)
        {
            Default();
            return;
        }
        IPicture picture = pictures[0];
        byte[] data = picture.Data.Data;
        Image = new Bitmap(new MemoryStream(data));
    }

    private void Default()
    {
        Image = ImageServer.DefaultImage;
    }
    public IImage? Image { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
    public int Id { get; set; }
}

public class MusicJsonModel
{
    public string Name { get; set; }
    public string Url { get; set; }
    public int Id { get; set; }

    public MusicJsonModel(string name, string url, int id = 1)
    {
        Name = name;
        Url = url;
        Id = id;
    }

    public MusicJsonModel(string url)
    {
        Url = url;
        Name = File.Exists(url) ? Path.GetFileNameWithoutExtension(url) : url;
        Id = 1;
    }
}