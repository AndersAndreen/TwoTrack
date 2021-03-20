using FluentAssertions;
using Xunit;

namespace Tests.ValueObject
{
    public class ValueObjectGetHashCodeTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        [InlineData(1, 1)]
        [InlineData(int.MinValue, int.MinValue)]
        [InlineData(int.MaxValue, int.MaxValue)]
        public void GetHashCode_SameInputMakesSameHashCode(int value1, int value2)
        {
            // Arrange

            // Act
            var hash1 = ValueObjectIntMock.Make(value1).GetHashCode();
            var hash2 = ValueObjectIntMock.Make(value2).GetHashCode();

            // Assert
            hash1.Should().Be(hash2);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(0, -1)]
        [InlineData(1, 2)]
        [InlineData(-1, -2)]
        [InlineData(int.MinValue, int.MinValue+1)]
        [InlineData(int.MaxValue, int.MaxValue-1)]
        public void GetHashCode_DifferentInputMakesDifferentHashCode(int value1, int value2)
        {
            // Arrange

            // Act
            var hash1 = ValueObjectIntMock.Make(value1).GetHashCode();
            var hash2 = ValueObjectIntMock.Make(value2).GetHashCode();

            // Assert
            hash1.Should().NotBe(hash2);
        }


    }
}
