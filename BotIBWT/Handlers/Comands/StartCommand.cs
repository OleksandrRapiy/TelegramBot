using IBWT.Framework.Abstractions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestIBWT.Handlers.Comands
{
    public class StartCommand : CommandBase
    {

        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args, CancellationToken cancellationToken)
        {
            var message = context.Update.Message;
            await context.Bot.Client.SendTextMessageAsync(
                        message.Chat,
                        "Bot successful started",
                        ParseMode.Default,
                        replyToMessageId: message.MessageId,
                        replyMarkup: new InlineKeyboardMarkup(
                            InlineKeyboardButton.WithCallbackData("Tap to start", "Start")
                            ),
                        cancellationToken: cancellationToken
                        );


            //await next(context, cancellationToken);
        }
    }
}
