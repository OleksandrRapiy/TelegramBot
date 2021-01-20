using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TelegramBotApp.Data;

namespace TelegramBotApp
{
    public class TelegramBotContext : DbContext
    {
        public DbSet<MessageReceiver> MessageReceivers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public TelegramBotContext(DbContextOptions<TelegramBotContext> options) : base(options)
        {
            Database.Migrate();
        }
    }
}
