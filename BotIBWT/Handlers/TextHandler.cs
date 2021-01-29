using BotIBWT.Services;
using IBWT.Framework.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotIBWT.Handlers
{
    public class TextHandler : IUpdateHandler
    {
        private readonly IMessageService _messageService;
        public TextHandler(IMessageService messageService)
        {
            _messageService = messageService;
        }
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            Message message = context.Update.Message;

            await context.Bot.Client.SendTextMessageAsync(
                message.Chat,
                "We will save your message, please wait for response: \n" + message.Text,
                cancellationToken: cancellationToken
            );


            await _messageService.AddNewMessageAsync(message);


        }
    }
}
