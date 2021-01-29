using IBWT.Framework;
using IBWT.Framework.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotIBWT.Handlers
{
    public class CallbackHandler : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            CallbackQuery callback = context.Update.CallbackQuery;

            await context.Bot.Client.SendTextMessageAsync(context.Update.GetChatId(), "The hidden data from Button: \n" + callback.Data);

            //await next(context, cancellationToken);

        }
    }
}
