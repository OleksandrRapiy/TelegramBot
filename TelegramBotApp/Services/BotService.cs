using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBotApp.Configurationss;

namespace TelegramBotApp.Services
{
    public class BotService : IBotService
    {
        public TelegramBotClient Client { get; }

        private readonly BotConfiguration _options;
        public BotService(IOptions<BotConfiguration> options)
        {
            _options = options.Value;

            Client ??= new TelegramBotClient(_options.Token);
        }
    }
}
