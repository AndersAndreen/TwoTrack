using System.Linq;
using FluentAssertions;
using TwoTrack.Core.Defaults;
using Xunit;

namespace TwoTrack.Core.UnitTests.TtResultTests
{
    public class SetExceptionFilterTests
    {
        [Fact]
        public void SetExceptionFilter_nullArgument_NullArgumentError()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok().SetExceptionFilter(null);

            // Assert
            result.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void SetExceptionFilter_ExpectOk()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok().SetExceptionFilter(ex => false);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
    }
}
