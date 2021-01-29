namespace BotIBWT.Data
{
    public class MessageReceiver : Entity
    {
        public long ChatId { get; set; }
        public string Receiver { get; set; }
    }
}
