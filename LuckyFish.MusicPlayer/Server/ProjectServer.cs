using System.IO;
using LuckyFish.MusicPlayer.Models;
using Newtonsoft.Json;

namespace LuckyFish.MusicPlayer.Server;

public static class ProjectServer
{
    private static string Position => ImageServer.CodePath + "\\Assets\\project.json";
    public static ProjectInfo? Read()
        => JsonConvert.DeserializeObject<ProjectInfo>(File.ReadAllText(Position));

    public static void Write(ProjectInfo context)
        => File.WriteAllText(Position,JsonConvert.SerializeObject(context));
}