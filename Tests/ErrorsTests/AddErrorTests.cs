using FluentAssertions;
using System.Linq;
using TwoTrackResult;
using TwoTrackResult.Defaults;
using Xunit;

namespace Tests.ErrorsTests
{
    public class AddErrorTests
    {
        [Fact]
        public void AddNullToSucceded_ExpectArgumentNullError()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok().AddError(null);

            // Assert
            result.Errors.First().Category.Should().Be(Category.ArgumentNullError);
        }

        [Fact]
        public void AddNullToSucceded_ExpectImmutability()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok();
            var result2 = result.AddError(null);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
        }

        [Fact]
        public void AddErrorToFailed_ExpectImmutability()
        {
            // Arrange
            // Act
            var result = TwoTrack.Fail();
            var result2 = result.AddError(null);

            // Assert
            result.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(2);
        }

        [Theory]
        [InlineData(ErrorLevel.Warning)]
        [InlineData(ErrorLevel.ReportWarning)]
        public void AddWarnings_ExpectResultSucceeded(ErrorLevel errrorLevel)
        {
            // Arrange
            var error = TtError.Make(errrorLevel);

            // Act
            var result = TwoTrack.Ok().AddError(error);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Succeeded.Should().BeTrue();
        }

        [Theory]
        [InlineData(ErrorLevel.Error)]
        [InlineData(ErrorLevel.Critical)]
        [InlineData(ErrorLevel.ReportError)]
        public void AddWarnings_ExpectResultFailed(ErrorLevel errrorLevel)
        {
            // Arrange
            var error = TtError.Make(errrorLevel);

            // Act
            var result = TwoTrack.Ok().AddError(error);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Succeeded.Should().BeFalse();
        }
    }
}
