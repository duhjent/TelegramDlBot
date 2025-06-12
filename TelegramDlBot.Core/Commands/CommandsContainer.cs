using Telegram.Bot.Types;
using TelegramDlBot.Core.Contracts;

namespace TelegramDlBot.Core.Commands
{
    internal class CommandsContainer(IEnumerable<BotCommand> commands) : ICommandsContainer
    {
        public IEnumerable<BotCommand> Commands { get; } = commands;
    }
}
