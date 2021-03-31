using System.Linq;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
using Xunit;

namespace TwoTrackCoreUnitTests.ErrorsTests
{
    public class TtResultAddErrorTests
    {
        [Fact]
        public void AddNullToSucceded_ExpectArgumentNullError()
        {
            // Arrange
            // Act
            var result = TwoTrackCore.TwoTrack.Ok().AddError(null);

            // Assert
            result.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void AddNullToSucceded_ExpectImmutability()
        {
            // Arrange
            // Act
            var result = TwoTrackCore.TwoTrack.Ok();
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
            var result = TwoTrackCore.TwoTrack.Fail();
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
            var result = TwoTrackCore.TwoTrack.Ok().AddError(error);

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
            var result = TwoTrackCore.TwoTrack.Ok().AddError(error);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Succeeded.Should().BeFalse();
        }
    }
}
