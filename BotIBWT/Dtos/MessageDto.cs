using BotIBWT.Dtos;
using System.Collections.Generic;

namespace BotIBWT.Dtos
{
    public class MessageDto
    {
        public string MessageText { get; set; }
        public long ChatId { get; set; }

        public IEnumerable<ButtonDto> Buttons { get; set; }
    }
}
