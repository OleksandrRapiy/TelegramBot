using Microsoft.EntityFrameworkCore;
using TestIBWT.Data;

namespace BotIBWT
{
    public class BotIBWTContext: DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageReceiver> MessageReceivers { get; set; }

        public BotIBWTContext(DbContextOptions<BotIBWTContext> options): base(options)
        {
            //Database.Migrate();
        }
    }
}
