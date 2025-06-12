using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramDlBot.Core.Contracts;

namespace TelegramDlBot.Core.Commands
{
    [TelegramCommand("start", "Start")]
    internal sealed class StartCommand(ITelegramBotClient bot) : ICommand
    {
        private readonly ITelegramBotClient _bot = bot;

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await _bot.SendMessage(message.Chat, "Started", cancellationToken: cancellationToken);
        }
    }
}
