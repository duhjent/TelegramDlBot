using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramDlBot.Core.Contracts;

namespace TelegramDlBot.Core.Commands
{
    [TelegramCommand("help", "Get help")]
    internal sealed class HelpCommand(ITelegramBotClient bot) : ICommand
    {
        private readonly ITelegramBotClient _bot = bot;

        public async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            await _bot.SendMessage(message.Chat, "Help placeholder", cancellationToken: cancellationToken);
        }
    }
}
