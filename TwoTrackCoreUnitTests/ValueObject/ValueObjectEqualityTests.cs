using FluentAssertions;
using Xunit;

namespace TwoTrackCoreUnitTests.ValueObject
{
    public class ValueObjectEqualityTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData("#")]
        [InlineData(2.5)]
        [InlineData(false)]
        public void Equals_TwoObjectsOfDifferentType(object compareTo)
        {
            // Arrange
            var mock = ValueObjectIntMock.Make(1);

            // Act
            var result = mock.Equals(compareTo);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, false)]
        public void Equals_TwoObjectsOfSameType(int value, int compareTo, bool expectedResult)
        {
            // Arrange
            var mock = ValueObjectIntMock.Make(value);

            // Act
            var result = mock.Equals(ValueObjectIntMock.Make(compareTo));

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(0, 0, true)]
        [InlineData(0, 1, false)]
        [InlineData(1, 0, false)]
        public void EqualityOperator_TwoObjectsOfSameType(int value, int compareTo, bool expectedResult)
        {
            // Arrange
            var mock = ValueObjectIntMock.Make(value);

            // Act
            var result = mock == ValueObjectIntMock.Make(compareTo);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(0, 1, true)]
        [InlineData(1, 0, true)]
        public void InqualityOperator_TwoObjectsOfSameType(int value, int compareTo, bool expectedResult)
        {
            // Arrange
            var mock = ValueObjectIntMock.Make(value);

            // Act
            var result = mock != ValueObjectIntMock.Make(compareTo);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
