using System.Collections.Generic;
using FluentAssertions;
using TwoTrackResult;
using Xunit;

namespace Tests.Errors
{
    public class AddErrorsTests
    {
        [Fact]
        public void AddNull_ExpectResultFailed()
        {
            // Arrange
            var error = TtError.Make(ErrorLevel.Warning);

            // Act
            var result = TwoTrack.Ok().AddErrors(null);

            // Assert
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void AddErrorsToSucceded_Imutability_ExpectResultsToDiffer()
        {
            // Arrange
            var error = TtError.Make(ErrorLevel.Warning);
            var result = TwoTrack.Ok();
            var result2 = TwoTrack.Fail().AddError(TtError.Make(ErrorLevel.Critical));

            // Act
            var result3 = result.AddErrors(result2.Errors);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
            result3.Succeeded.Should().BeFalse();
            result3.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void AddNoErrorsToSucceded_ExpectResultSucceded()
        {
            // Arrange
            var error = TtError.Make(ErrorLevel.Warning);

            // Act
            var result = TwoTrack.Ok().AddErrors(new List<TtError>());

            // Assert
            result.Succeeded.Should().BeTrue();
        }
    }
}
