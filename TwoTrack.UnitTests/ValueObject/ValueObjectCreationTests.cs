using System;
using FluentAssertions;
using Xunit;

namespace TwoTrack.UnitTests.ValueObject
{
    public class ValueObjectIntCreationTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void CreateValueObjectTest(int value)
        {
            // Arrange

            // Act
            var mock = ValueObjectIntMock.Make(value);

            // Assert
            mock.Value.Should().Be(value);
        }
    }

    public class ValueObjectStringCreationTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("#")]
        [InlineData("Hej")]
        public void CreateValueObjectTest_success(string value)
        {
            // Arrange

            // Act
            var mock = ValueObjectStringMock.Make(value);

            // Assert
            mock.Value.Should().Be(value);
        }

        [Fact]
        public void CreateValueObjectTest_fail()
        {
            // Arrange
            Action act = () => ValueObjectStringMock.Make(null);

            // Act

            // Assert

            act.Should().Throw<ArgumentNullException>();
        }

    }
}
