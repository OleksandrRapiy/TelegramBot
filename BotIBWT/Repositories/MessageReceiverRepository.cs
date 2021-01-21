
using BotIBWT;
using TestIBWT.Data;
using TestIBWT.Repositories.BaseRepositories;

namespace TestIBWT.Repositories
{
    public class MessageReceiverRepository : BaseRepository<MessageReceiver>
    {

        public MessageReceiverRepository(BotIBWTContext context) : base(context)
        {

        }
    }
}
