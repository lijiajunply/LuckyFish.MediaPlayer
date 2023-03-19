using System.Text;
using LibVLCSharp.Shared;
using Newtonsoft.Json.Linq;

var key = GetData1("http://cloud-music.pl-fe.cn/login/qr/key")["data"]["unikey"].ToString();
var data = GetData1($"http://cloud-music.pl-fe.cn/login/qr/check?key={key}")["code"];
Console.WriteLine(data);

JObject GetData(Dictionary<string,string> data,string url)
{
    StringBuilder builder = new StringBuilder("?");
    foreach (var v in data)
        builder.Append($"{v.Key}={v.Value}&");
    string order = url+builder.ToString(0,builder.Length-1);
    string context = new HttpClient().GetStringAsync(order).Result;
    return JObject.Parse(context);
}

JObject GetData1(string url)
    => JObject.Parse(new HttpClient().GetStringAsync(url).Result);

void MusicPlay()
{
    using var libvlc = new LibVLC(enableDebugLogs: true);
    using var media = new Media(libvlc, new Uri(@"C:\Users\李嘉俊\Downloads\米店.mp3"));
    using MediaPlayer player = new MediaPlayer(media);
    player.PositionChanged += (sender, e) => Console.WriteLine(e.Position * 100 + "%");
    player.Play();
    Console.ReadKey();
}