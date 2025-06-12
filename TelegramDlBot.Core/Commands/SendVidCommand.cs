using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramDlBot.Core.Contracts;

namespace TelegramDlBot.Core.Commands
{
    [TelegramCommand("sendvid", "Send video")]
    internal sealed class SendVidCommand(ITelegramBotClient bot) : ICommand
    {
        private readonly ITelegramBotClient _bot = bot;

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await _bot.SendVideo(message.Chat, File.OpenRead(@"E:\object-detection\data\drone_cows_cut.mp4"), cancellationToken: cancellationToken);
        }
    }
}
