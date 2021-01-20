using TelegramBotApp.Data;
using TelegramBotApp.Repositories.BaseRepositories;

namespace TelegramBotApp.Repositories
{
    public class MessageRepository: BaseRepository<Message>
    {
        public MessageRepository(TelegramBotContext context): base(context)
        {
        }
    }
}
