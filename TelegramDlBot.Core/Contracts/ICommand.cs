using Telegram.Bot.Types;

namespace TelegramDlBot.Core.Contracts
{
    public interface ICommand
    {
        Task HandleMessageAsync(Message message, CancellationToken cancellationToken);
    }
}
