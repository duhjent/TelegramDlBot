using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramDlBot.Core.Contracts;
using TelegramDlBot.Core.YoutubeDownload;

namespace TelegramDlBot.Core.Commands
{
    [TelegramCommand("ytdl", "Download from YouTube")]
    internal class DownloadFromYoutubeCommand(ITelegramBotClient bot, IYouTubeDownloader youtubeDownloader) : ICommand
    {
        private readonly ITelegramBotClient _bot = bot;
        private readonly IYouTubeDownloader _youtubeDownloader = youtubeDownloader;

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var command = message.Entities!.Single(e => e.Type == MessageEntityType.BotCommand);
            var cmdParams = message.Text!.Substring(command.Length + 1); // We assume the commands offset is 0

            using var videoStream = await _youtubeDownloader.DownloadAsync(cmdParams, cancellationToken);
            await _bot.SendVideo(message.Chat, videoStream, cancellationToken: cancellationToken);
        }
    }
}
