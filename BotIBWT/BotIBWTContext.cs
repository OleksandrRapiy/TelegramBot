using Microsoft.EntityFrameworkCore;
using BotIBWT.Data;

namespace BotIBWT
{
    public class BotIBWTContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageReceiver> MessageReceivers { get; set; }

        public BotIBWTContext(DbContextOptions<BotIBWTContext> options) : base(options)
        {
        }
    }
}
