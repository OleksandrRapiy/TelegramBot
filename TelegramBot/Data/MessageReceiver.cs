namespace TelegramBotApp.Data
{
    public class MessageReceiver: Entity
    {
        public long ChatId { get; set; }
        public string Receiver { get; set; }

        public MessageReceiver()
        { }

        public MessageReceiver(long chatId, string receiver)
        {
            ChatId = chatId;
            Receiver = receiver;
        }
    }
}
