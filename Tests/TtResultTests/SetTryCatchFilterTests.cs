using FluentAssertions;
using System.Linq;
using TwoTrackResult;
using Xunit;

namespace Tests.TtResultTests
{
    public class SetTryCatchFilterTests
    {
        [Fact]
        public void SetTryCatchFilter_nullArgument_NullArgumentError()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok().SetTryCatchFilter(null);

            // Assert
            result.Errors.First().Category.Should().Be(TwoTrackResult.Defaults.Category.ArgumentNullError);
        }

        [Fact]
        public void SetTryCatchFilter_ExpectOk()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok().SetTryCatchFilter(ex => false);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
    }
}
