using FluentAssertions;
using TwoTrackResult;
using Xunit;

namespace Tests.Errors
{
    public class AddErrorTests
    {
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
