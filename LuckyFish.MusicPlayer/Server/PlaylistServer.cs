using System.IO;
using System.Linq;
using LuckyFish.MusicPlayer.Models;
using Newtonsoft.Json;

namespace LuckyFish.MusicPlayer.Server;

public static class PlaylistServer
{
    private static string Position => ImageServer.CodePath + "\\Assets\\playlist.json";

    public static PlaylistModel[]? Read()
    {
        var a = JsonConvert.DeserializeObject<PlaylistArray>(File.ReadAllText(Position));
        if (a is { Data: null }) return null;
        var b = a?.Data.Select(x => new PlaylistModel(x.Name,x.Url)
        {
            Musics = x.Musics.Select(y => new MusicModel(y.Name,y.Url,y.Id)).ToList()
        }).ToArray();
        return b;
    }

    public static void Write(PlaylistJsonModel[] context)
    {
        var data = new PlaylistArray() { Data = context };
        File.WriteAllText(Position, JsonConvert.SerializeObject(data));
    }
    public static void Write(PlaylistModel[] context)
    {
        var data = new PlaylistArray(context.
            Select(x => new PlaylistJsonModel(x.Name, x.Url)).ToArray());
        File.WriteAllText(Position, JsonConvert.SerializeObject(data));
    }

}

public class PlaylistArray
{
    public PlaylistJsonModel[]? Data { get; set; }
    public PlaylistArray(){}
    public PlaylistArray(PlaylistJsonModel[]? data) => Data = data;
}