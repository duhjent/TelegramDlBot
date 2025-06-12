using Microsoft.Extensions.DependencyInjection;
using TelegramDlBot.Core.Contracts;

namespace TelegramDlBot.Core.Commands
{
    internal class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand? GetCommand(string name)
        {
            return _serviceProvider.GetKeyedService<ICommand>(name);
        }
    }
}
