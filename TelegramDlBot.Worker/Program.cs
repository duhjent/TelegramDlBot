using Telegram.Bot;
using TelegramDlBot.Core;
using TelegramDlBot.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<ITelegramBotClient>(_ => new TelegramBotClient(builder.Configuration["BotConfig:Token"]));
builder.Services.AddTelegramCommandProcessor();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

