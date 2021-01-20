using System.Collections.Generic;
using TelegramBotApp.Data;

namespace TelegramBotApp.Dtos
{
    public class MessageDto
    {
        public string MessageText { get; set; }
        public long ChatId { get; set; }

        public IEnumerable<string> Buttons { get; set; }
    }
}
