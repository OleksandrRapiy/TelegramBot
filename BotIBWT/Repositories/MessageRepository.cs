using BotIBWT;
using BotIBWT.Data;
using BotIBWT.Repositories.BaseRepositories;

namespace BotIBWT.Repositories
{
    public class MessageRepository : BaseRepository<Message>
    {
        public MessageRepository(BotIBWTContext context) : base(context)
        {
        }
    }
}
