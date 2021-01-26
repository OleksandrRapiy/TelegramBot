using AutoFixture;
using BotIBWT.Tests.FileToTests;
using System;
using System.Collections.Generic;
using TestIBWT.Data;
using Xunit;

namespace BotIBWT.Tests
{
    public class XUnitTests
    {
        [Fact]
        public void Math_XUnit_Test()
        {
            // Arrange - устанавливает начальные условия для выполнения теста 
            var math = new Mathematic();

            // Act - выполняет тест (обычно представляет одну строку кода)
            var result = math.AddNumbers(5, 45);

            // Assert - верифицирует результат теста
            Assert.Equal(50, result);
        }

        [Theory]
        [InlineData(12, -2, 12)]
        public void IsValid_MessageReceiver_Test(int id, long chatId, int expectedId)
        {
            var fixture = new Fixture();
            var sut = fixture.Build<MessageReceiver>()
                       .With(x => x.Id, id)
                       .With(x => x.ChatId, chatId).Create();

            Assert.IsType<long>(sut.ChatId);
            Assert.Equal(expectedId, sut.Id);
        }


        [Theory]
        [InlineData(10, -10, 20)]
        [MemberData(nameof(DataToTest))]
        public void Math_XUnit_ByTheory(int expected, int a, int b)
        {
            var math = new Mathematic();

            var result = math.AddNumbers(a, b);

            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> DataToTest()
        {

            yield return new object[] { 10, 5, 5 };
            yield return new object[] { 100, -50, 150 };
            yield return new object[] { 1_000_000, 500_000, 500_000 };

        }
    }
}
