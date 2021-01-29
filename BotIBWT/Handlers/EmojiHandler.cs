using IBWT.Framework.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace BotIBWT.Handlers
{
    public class EmojiHandler : IUpdateHandler
    {
        public async Task HandleAsync(IUpdateContext context, UpdateDelegate next, CancellationToken cancellationToken)
        {
            Message message = context.Update.Message;

            StickerSet stickerSet = await context.Bot.Client.GetStickerSetAsync("EvilMinds", cancellationToken);

            Sticker similarEvilMindSticker = stickerSet.Stickers.ElementAt(new Random().Next(0, stickerSet.Stickers.Count()));

            Sticker replySticker = similarEvilMindSticker ?? stickerSet.Stickers.First();

            await context.Bot.Client.SendStickerAsync(
                message.Chat,
                replySticker.FileId,
                replyToMessageId: message.MessageId,
                cancellationToken: cancellationToken

            );

            //await next(context, cancellationToken);
        }
    }
}
