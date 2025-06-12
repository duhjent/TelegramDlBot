using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Telegram.Bot.Types;
using TelegramDlBot.Core.Commands;
using TelegramDlBot.Core.Contracts;
using TelegramDlBot.Core.YoutubeDownload;

namespace TelegramDlBot.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramCommandProcessor(this IServiceCollection services)
        {
            services.AddScoped<ICommandFactory, CommandFactory>();

            var commands = Assembly
                .GetAssembly(typeof(ICommandFactory))!
                .GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(typeof(ICommand)))
                .Select(t => (Type: t, Attr: t.GetCustomAttribute<TelegramCommandAttribute>()!));

            services.AddSingleton<ICommandsContainer>(new CommandsContainer(commands.Select(x => new BotCommand(x.Attr.Name, x.Attr.Description))));

            foreach (var (type, key) in commands.Select(c => (c.Type, c.Attr.Name)))
            {
                services.AddKeyedScoped(typeof(ICommand), key, type);
            }

            services.AddSingleton<IYouTubeDownloader, YtDlpDownloader>();

            return services;
        }
    }
}
