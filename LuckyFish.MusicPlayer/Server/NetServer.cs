using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LuckyFish.MusicPlayer.Server;

public static class NetServer
{
    public static MusicApi NetEaseUrl => new MusicApi("http://cloud-music.pl-fe.cn/");
    public static MusicApi QQMusicUrl => new MusicApi("http://api.qq.jsososo.com/");

    public static JObject GetData(Dictionary<string,string> data,string url)
    {
        var handler = new HttpClientHandler();

        handler.ServerCertificateCustomValidationCallback +=
            (sender, certificate, chain, errors) => true;
        StringBuilder builder = new StringBuilder("?");
        foreach (var v in data)
            builder.Append($"{v.Key}={v.Value}&");
        string order = url+builder.ToString(0,builder.Length-1);
        string context = new HttpClient().GetStringAsync(order).Result;
        return JObject.Parse(context);
    }

    public static JObject GetData(string url)
    {
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback +=
            (sender, certificate, chain, errors) => true;
        return JObject.Parse(new HttpClient().GetStringAsync(url).Result);
    } 
    

    public static void PostData(JObject obj)
    {
        
    }
}

public class MusicApi
{
    public MusicApi(string url, string loginCreate, string loginQrCode, string loginCheck, string logOut, string userPlaylist, string playlist, string lyric, string newSong,string userInfo)
    {
        Url = url;
        LoginCreate = loginCreate;
        LoginQrCode = loginQrCode;
        LoginCheck = loginCheck;
        LogOut = logOut;
        UserPlaylist = userPlaylist;
        Playlist = playlist;
        Lyric = lyric;
        NewSong = newSong;
        UserInfo = userInfo;
    }
    public MusicApi(string url)
    {
        Url = url;
        LoginCreate = "login/qr/key";
        LoginQrCode = "login/qr/create";
        LoginCheck = "login/qr/check";
        LogOut = "logout";
        UserPlaylist = "user/playlist";
        Playlist = "playlist/update";
        Lyric = "lyric";
        NewSong = "personalized/newsong";
        UserInfo = "user/subcount";
    }

    public string Url { get; set; }
    public string LoginCreate { get; set; }
    public string LoginQrCode { get; set; }
    public string LoginCheck { get; set; }
    public string LogOut { get; set; }
    public string UserInfo { get; set; }
    public string UserPlaylist { get; set; }
    public string Playlist { get; set; }
    public string Lyric { get; set; }
    public string NewSong { get; set; }

}

/*
 *
 * url : http://cloud-music.pl-fe.cn/
 * login key : /login/qr/key
 * login qrcode : /login/qr/create?key={}&qrimg={}
 * check : /login/qr/check?key={}
 * LogOut : /logout
 * user playlist : /user/playlist?uid={}
 * playlist : /playlist/update
 * lyric : /lyric?id={}
 * newsong : /personalized/newsong (?limit={})
 */