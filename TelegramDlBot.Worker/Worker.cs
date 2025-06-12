using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramDlBot.Core.Contracts;

namespace TelegramDlBot.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ITelegramBotClient _bot;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ICommandsContainer _commandsContainer;

        public Worker(ILogger<Worker> logger,
            ITelegramBotClient bot,
            IServiceScopeFactory scopeFactory,
            ICommandsContainer commandsContainer)
        {
            _logger = logger;
            _bot = bot;
            _scopeFactory = scopeFactory;
            _commandsContainer = commandsContainer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bot.SetMyCommands(_commandsContainer.Commands, cancellationToken: stoppingToken);

            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool, so we use cancellation token
            _bot.StartReceiving(
                updateHandler: HandleUpdate,
                errorHandler: HandleError,
                cancellationToken: stoppingToken
            );
        }

        private async Task HandleUpdate(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
        {
            var task = update.Type switch
            {
                UpdateType.Message => HandleMessage(update.Message!, cancellationToken),
                _ => Task.CompletedTask
            };

            await task;
        }

        private async Task HandleMessage(Message message, CancellationToken cancellationToken)
        {
            if (message.Entities![0].Type != MessageEntityType.BotCommand)
            {
                await _bot.SendMessage(message.Chat, "No command recognized");
                return;
            }

            var commandEntity = message.Entities[0];
            var command = message.Text!.Substring(commandEntity.Offset, commandEntity.Length).Replace("/", "");

            using var scope = _scopeFactory.CreateScope();
            var commandFactory = scope.ServiceProvider.GetRequiredService<ICommandFactory>();
            var cmd = commandFactory.GetCommand(command);
            if (cmd != null)
            {
                try
                {
                    await cmd.HandleMessageAsync(message, cancellationToken);
                } catch (Exception ex)
                {
                    _logger.LogError("Error happened with client {}, message is {}, error is {}", message.Chat, message.Text, ex);
                }
            }
        }

        private Task HandleError(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError("Error happened: {}", exception);

            return Task.CompletedTask;
        }
    }
}
