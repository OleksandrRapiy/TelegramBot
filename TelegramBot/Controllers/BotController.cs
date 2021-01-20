using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.Data;
using TelegramBotApp.Dtos;
using TelegramBotApp.Repositories.BaseRepositories;
using TelegramBotApp.Services;

namespace TelegramBotApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotService _botService;
        private readonly IBaseRepository<MessageReceiver> _messageReceiverRepository;
        private readonly IBaseRepository<TelegramBotApp.Data.Message> _messageRepository;

        public BotController(
            IBotService botService,
            IBaseRepository<MessageReceiver> messageReceiverRepository,
            IBaseRepository<TelegramBotApp.Data.Message> messageRepository
        )
        {
            _botService = botService;
            _messageReceiverRepository = messageReceiverRepository;
            _messageRepository = messageRepository;
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {

            var message = update.Message;
            try
            {
                if (message.Type == MessageType.Text)
                {
                    var receiver = await _messageReceiverRepository.GetByExpressionAsync(x => x.ChatId == message.Chat.Id && x.Receiver == message.Chat.Username);

                    if (receiver == null)
                    {
                        receiver = await _messageReceiverRepository.AddAsync(new MessageReceiver(message.Chat.Id, message.Chat.Username));
                    }

                    await _messageRepository.AddAsync(new Data.Message() { MessageReceiver = receiver, MessageText = message.Text, Date = DateTime.Now });

                    await _botService.Client.SendTextMessageAsync(message.Chat.Id, "\U0001F609");
                }
            }
            catch (Exception ex)
            {       
                await _botService.Client.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "\U0001F92F");
            }

            return Ok();
        }


        [HttpPost("send-message")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
        {
            var buttons = new InlineKeyboardMarkup(messageDto.Buttons.Select(x => new List<InlineKeyboardButton>()
                         {
                             InlineKeyboardButton.WithCallbackData(x)
                         }));

            await _botService.Client.SendTextMessageAsync(messageDto.ChatId, messageDto.MessageText, ParseMode.Default, false, false, 0, buttons);
            return Ok();
        }

        [HttpGet("receivers")]
        public async Task<IActionResult> GetReceivers()
        {
            var messageReceivers = await _messageReceiverRepository.GetAllAsync();

            return Ok(messageReceivers);
        }


        [HttpGet("messages/{chatId}")]
        public async Task<IActionResult> GetMessages([FromRoute] long chatId)
        {
            var messages = await _messageRepository.Collections.Include(x => x.MessageReceiver)
                                .Where(x => x.MessageReceiver.ChatId == chatId).ToListAsync();

            return Ok(messages);
        }
    }
}