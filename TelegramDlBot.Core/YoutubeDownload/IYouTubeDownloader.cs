using System.Net.Http.Headers;

namespace TelegramDlBot.Core.YoutubeDownload
{
    internal interface IYouTubeDownloader
    {
        Task<Stream> DownloadAsync(string url, CancellationToken cancellationToken);
    }
}
