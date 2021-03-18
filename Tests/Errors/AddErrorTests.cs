using FluentAssertions;
using TwoTrackResult;
using Xunit;

namespace Tests.Errors
{
    public class AddErrorTests
    {
        [Fact]
        public void AddNull_ExpectResultFailed()
        {
            // Arrange
            var error = TtError.Make(ErrorLevel.Warning);

            // Act
            var result = TwoTrack.Ok().AddError(null);

            // Assert
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void AddErrorToSucceded_Imutability_ExpectResultsToDiffer()
        {
            // Arrange
            var error = TtError.Make(ErrorLevel.Warning);

            // Act
            var result = TwoTrack.Ok();
            var result2 = result.AddError(null);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
        }

        [Fact]
        public void AddErrorToFailed_Imutability_ExpectErrorCountToDiffer()
        {
            // Arrange
            var error = TtError.Make(ErrorLevel.Warning);

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
