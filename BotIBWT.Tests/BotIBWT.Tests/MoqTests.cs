using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotIBWT.Data;
using BotIBWT.Repositories;
using BotIBWT.Repositories.BaseRepositories;
using Xunit;
using Xunit.Abstractions;

namespace BotIBWT.Tests
{
    public class MoqTests
    {
        private readonly ITestOutputHelper _output;
        public MoqTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task Count_Receivers_Test()
        {
            var messageRepoMock = new Mock<IBaseRepository<MessageReceiver>>();

            messageRepoMock.Setup(x => x.GetAllAsync().Result).Returns(GetReceivers());

            var expected = GetReceivers();
            var result = await messageRepoMock.Object.GetAllAsync();

            Assert.True(result != null);

            Assert.Equal(expected.Count, result.Count());
        }

        [Fact]
        public async Task Add_New_MessageReceiver_Test()
        {
            var mock = new Mock<IBaseRepository<MessageReceiver>>();
            mock.Setup(x => x.AddAsync(It.IsAny<MessageReceiver>()).Result)
                       .Returns(GetEntity());

            var result = await mock.Object.AddAsync(GetEntity());
            var expected = GetEntity();


            Assert.NotNull(result);
            Assert.Equal(result.Id, expected.Id);
            Assert.IsType<MessageReceiver>(result);
        }

        private List<MessageReceiver> GetReceivers()
        {
            return new List<MessageReceiver>()
            {
                new MessageReceiver()
                {
                    ChatId =  454,
                    Receiver = "cosmicam_accidente"
                },
                new MessageReceiver()
                {
                    ChatId =  455,
                    Receiver = "user_telegram"
                }
            };
        }

        private MessageReceiver GetEntity()
        {
            return new MessageReceiver()
            {
                Id = 2,
                ChatId = 454,
                Receiver = "cosmicam_accidente"
            };
        }
    }
}
