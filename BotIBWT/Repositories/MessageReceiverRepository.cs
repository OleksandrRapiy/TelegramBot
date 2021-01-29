
using BotIBWT;
using BotIBWT.Data;
using BotIBWT.Repositories.BaseRepositories;

namespace BotIBWT.Repositories
{
    public class MessageReceiverRepository : BaseRepository<MessageReceiver>
    {

        public MessageReceiverRepository(BotIBWTContext context) : base(context)
        {

        }
    }
}
