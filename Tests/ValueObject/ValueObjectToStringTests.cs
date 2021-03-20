using FluentAssertions;
using Xunit;

namespace Tests.ValueObject
{
    public class ValueObjectToStringTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void ToStringTest(int value)
        {
            // Arrange

            // Act
            var mock = ValueObjectIntMock.Make(value);

            // Assert
            mock.ToString().Should().Be(value.ToString());
        }
    }
}
