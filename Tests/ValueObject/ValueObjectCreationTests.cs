using FluentAssertions;
using Xunit;

namespace Tests.ValueObject
{
    public class ValueObjectCreationTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void CreateValueObjectTest(int value)
        {
            // Arrange

            // Act
            var mock = ValueObjectMockInt.Make(value);

            // Assert
            mock.Value.Should().Be(value);
        }
    }
}
