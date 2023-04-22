using System.IO;
using LuckyFish.MusicPlayer.Models;
using Newtonsoft.Json;

namespace LuckyFish.MusicPlayer.Server;

public static class PlaylistServer
{
    private static string Position => ImageServer.CodePath + "\\Assets\\playlist.json";
    public static PlaylistModel? Read()
     => JsonConvert.DeserializeObject<PlaylistModel>(File.ReadAllText(Position));

    public static void Write(PlaylistModel context)
        => File.WriteAllText(Position,JsonConvert.SerializeObject(context));
    
}