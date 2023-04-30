using System;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;

namespace LuckyFish.MusicPlayer.Server;

public static class ImageServer
{
    private static readonly bool IsProductionEnvironment = true;
    public static string CodePath
    {
        get
        {
            if (!IsProductionEnvironment) return "";
            var directory = AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
            var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
            return Path.Combine(slice.ToArray());
        }
    }
    /// <summary>
    /// 播放键的图片
    /// </summary>
    public static IImage PlayImage => new Bitmap(CodePath + "\\Assets\\PlayingSetting\\Play.png");
    /// <summary>
    /// 暂停键
    /// </summary>
    public static IImage PauseImage => new Bitmap(CodePath + "\\Assets\\PlayingSetting\\Pause.png");
    /// <summary>
    /// 列表循环
    /// </summary>
    public static IImage ListLoop => new Bitmap(CodePath + "\\Assets\\PlayingSetting\\ListLoop.png");
    /// <summary>
    /// 单曲
    /// </summary>
    public static IImage Single => new Bitmap(CodePath + "\\Assets\\PlayingSetting\\Single.png");
    /// <summary>
    /// 单曲循环
    /// </summary>
    public static IImage SingleLoop => new Bitmap(CodePath + "\\Assets\\PlayingSetting\\SingleLoop.png");
    /// <summary>
    /// 随机循环
    /// </summary>
    public static IImage RandomLoop => new Bitmap(CodePath + "\\Assets\\PlayingSetting\\RandomLoop.png");
    /// <summary>
    /// 默认图片
    /// </summary>
    public static IImage DefaultImage => new Bitmap(CodePath + "\\Assets\\Default.png");
    public static string? Root => Path.GetPathRoot(CodePath);
}