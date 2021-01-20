using TelegramBotApp.Data;
using TelegramBotApp.Repositories.BaseRepositories;

namespace TelegramBotApp.Repositories
{
    public class MessageReceiverRepository : BaseRepository<MessageReceiver>
    {

        public MessageReceiverRepository(TelegramBotContext context) : base(context)
        {

        }
    }
}
