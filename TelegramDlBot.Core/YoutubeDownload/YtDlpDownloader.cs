using System.Diagnostics;

namespace TelegramDlBot.Core.YoutubeDownload
{
    internal class YtDlpDownloader : IYouTubeDownloader
    {
        private const string ARGS_TEMPLATE = "{0} --merge-output-format mp4 --remux-video mp4 -S \"filesize:50M\" -o {1}";
        private const string DOWNLOADER_PATH = "yt-dlp";

        public async Task<Stream> DownloadAsync(string url, CancellationToken cancellationToken)
        {
            var fileName = Guid.NewGuid().ToString() + ".mp4";
            var args = string.Format(ARGS_TEMPLATE, url, fileName);
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true,
                FileName = DOWNLOADER_PATH,
                Arguments = args
            };

            using var process = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true,
            };

            process.Start();
            await process.WaitForExitAsync(cancellationToken);

            return File.OpenRead(fileName);
        }
    }
}
