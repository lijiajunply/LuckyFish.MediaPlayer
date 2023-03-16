using LibVLCSharp.Shared;


using var libvlc = new LibVLC(enableDebugLogs: true);
using var media = new Media(libvlc, new Uri(@"C:\Users\李嘉俊\Downloads\米店.mp3"));
using MediaPlayer player = new MediaPlayer(media);
player.Playing += (sender, eventArgs) =>
{
    Console.WriteLine(sender.GetType());
};
player.Play();

Console.ReadKey();