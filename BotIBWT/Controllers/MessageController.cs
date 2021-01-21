using System.Threading.Tasks;
using BotIBWT.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestIBWT.Data;
using TestIBWT.Dtos;
using TestIBWT.Repositories.BaseRepositories;

namespace BotIBWT.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IBaseRepository<Message> _messageRepository;
        private readonly IBaseRepository<MessageReceiver> _messageReceiversRepository;
        public MessageController(
            IMessageService messageService,
            IBaseRepository<MessageReceiver> messageReceiversRepository,
            IBaseRepository<Message> messageRepository
        )
        {
            _messageService = messageService;
            _messageReceiversRepository = messageReceiversRepository;
            _messageRepository = messageRepository;
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetMessage([FromRoute] long chatId)
        {
            var messages = await _messageRepository.FindAsync(x => x.MessageReceiver.ChatId == chatId, include: source => source.Include(x => x.MessageReceiver));

            return Ok(messages);

        }

        [HttpGet("receivers")]
        public async Task<IActionResult> GetReceivers()
        {
            var receivers = await _messageReceiversRepository.GetAllAsync();
            return Ok(receivers);
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto message)
        {
            await _messageService.SendMessage(message);

            return Ok();
        }
    }
}