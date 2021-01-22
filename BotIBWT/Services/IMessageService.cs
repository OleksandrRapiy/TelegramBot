using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestIBWT.Data;
using TestIBWT.Dtos;

namespace BotIBWT.Services
{
    public interface IMessageService
    {
        Task AddNewMessageAsync(Telegram.Bot.Types.Message message);
        Task SendMessageAsync(MessageDto message);
    }
}
