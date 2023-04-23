using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using LuckyFish.MusicPlayer.Server;
using Newtonsoft.Json.Linq;

namespace LuckyFish.MusicPlayer.ViewModels;

public class LoginViewModel : ViewModelBase
{
    public string Key { get; set; }
    private IImage _loginImage;
    public IImage LoginImage
    {
        get => _loginImage;
        set => SetField(ref _loginImage, value);
    }
    public string LoginInfo { get; set; }

    public LoginViewModel()
    {
        Init("NetEaseLogin");
    }

    public LoginViewModel(string loginInfo)
    {
        Init(loginInfo);
    }

    private void Init(string loginInfo)
    {
        LoginInfo = loginInfo;
        MusicApi api = LoginInfo == "NetEaseLogin" ? NetServer.NetEaseUrl : NetServer.QQMusicUrl;
        Key = NetServer.GetData(api.Url+api.LoginCreate)["data"]?["unikey"].ToString();
        if (Key != null)
        {
            JObject context = NetServer.GetData(new Dictionary<string, string>() { { "key", Key },{"qrimg","1"} },api.Url+api.LoginQrCode);
            var base64 = context["data"]?["qrimg"].ToString();
            base64 = base64.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");//将base64头部信息替换
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream memStream = new MemoryStream(bytes);
            LoginImage = new Bitmap(memStream);
        }
    }
}