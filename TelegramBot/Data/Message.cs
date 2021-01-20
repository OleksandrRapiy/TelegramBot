using System;

namespace TelegramBotApp.Data
{
    public class Message : Entity
    {
        public MessageReceiver MessageReceiver { get; set; }
        public string MessageText { get; set; }
        public DateTime Date { get; set; }
    }
}
