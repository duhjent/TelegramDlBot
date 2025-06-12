namespace TelegramDlBot.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand? GetCommand(string name);
    }
}
