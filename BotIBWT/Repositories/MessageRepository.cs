using BotIBWT;
using TestIBWT.Data;
using TestIBWT.Repositories.BaseRepositories;

namespace TestIBWT.Repositories
{
    public class MessageRepository: BaseRepository<Message>
    {
        public MessageRepository(BotIBWTContext context): base(context)
        {
        }
    }
}
