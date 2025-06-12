using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramDlBot.Core.Contracts
{
    public interface ICommandsContainer
    {
        public IEnumerable<BotCommand> Commands { get; }
    }
}
