using IBWT.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using BotIBWT.Data;
using BotIBWT.Dtos;
using BotIBWT.Repositories.BaseRepositories;

namespace BotIBWT.Services
{
    public class MessageService : IMessageService
    {
        private readonly IBaseRepository<Message> _messageRepository;
        private readonly IBaseRepository<MessageReceiver> _messageReceiverRepository;
        private readonly TelegramBot _telegramBot;

        public MessageService(
            IBaseRepository<Message> messageRepository,
            IBaseRepository<MessageReceiver> messageReceiverRepository,
                         TelegramBot telegramBot
        )
        {
            _messageRepository = messageRepository;
            _messageReceiverRepository = messageReceiverRepository;
            _telegramBot = telegramBot;
        }

        public async Task           AddNewMessageAsync(Telegram.Bot.Types.Message message)
        {
            var receiver = await _messageReceiverRepository.GetByExpressionAsync(x => x.ChatId == message.Chat.Id && x.Receiver == message.Chat.Username);

            if (receiver == null)
                receiver = await _messageReceiverRepository.AddAsync(new MessageReceiver() { ChatId = message.Chat.Id, Receiver = message.Chat.Username });

            var messageToCreate = new Message()
            {
                MessageText = message.Text,
                MessageReceiver = receiver,
                Date = DateTime.Now
            };

            await _messageRepository.AddAsync(messageToCreate);
        }

        public async Task SendMessageAsync(MessageDto message)
        {
            var markup = new InlineKeyboardMarkup(message.Buttons.Select(x =>
                            InlineKeyboardButton.WithCallbackData(x.Name, x.Action)
                        ));

            await _telegramBot.Client.SendTextMessageAsync(message.ChatId, message.MessageText, replyMarkup: markup);
        }
    }
}
