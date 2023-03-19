using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace LuckyFish.MusicPlayer.Server;

public static class ImageServer
{
    public static string CodePath
    {
        get
        {
            var directory = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
            var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
            return Path.Combine(slice.ToArray());
        }
    }
    public static IImage PlayImage => new Bitmap(CodePath + "\\Assets\\Play.jpg");
    public static IImage PauseImage => new Bitmap(CodePath + "\\Assets\\Pause.jpg");
    public static string Root => Path.GetPathRoot(CodePath);
}