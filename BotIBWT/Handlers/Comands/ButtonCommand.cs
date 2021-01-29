using IBWT.Framework.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotIBWT.Handlers.Comands
{
    public class ButtonCommand : CommandBase
    {
        public override async Task HandleAsync(IUpdateContext context, UpdateDelegate next, string[] args, CancellationToken cancellationToken)
        {
            var message = context.Update.Message;

            await context.Bot.Client.SendTextMessageAsync(
                        message.Chat,
                        "Your button list",
                        ParseMode.Default,
                        replyToMessageId: message.MessageId,
                        replyMarkup: new InlineKeyboardMarkup(
                                new List<InlineKeyboardButton>()
                                {
                                    InlineKeyboardButton.WithCallbackData("Show Message history", "history"),
                                    InlineKeyboardButton.WithCallbackData("Cancel", "cancel")
                                }
                            ),
                        cancellationToken: cancellationToken
                        );

            //await next(context, cancellationToken);
        }
    }
}
