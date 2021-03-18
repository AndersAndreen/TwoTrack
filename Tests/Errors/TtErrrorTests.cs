using FluentAssertions;
using TwoTrackResult;
using TwoTrackResult.Defaults;
using Xunit;

namespace Tests.Errors
{
    public class TtErrrorTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("cat", null)]
        [InlineData(null, "desc")]
        public void CategoryAndDescriptionNullChecks_ExpectResultFailed(string category, string description)
        {
            // Arrange

            // Act
            var error = TtError.Make(ErrorLevel.Warning, category, description);

            // Assert
            error.Level.Should().Be(ErrorLevel.Error);
            error.Category.Should().Be(ErrorCategory.ArgumentNullError);
            error.Description.Should().NotBeEmpty();
            error.StackTrace.Should().NotBeEmpty();
        }

        [Fact]
        public void AddError_ExpectCategoryAndDescriptionAdded()
        {
            // Arrange
            var category = "cat";
            var description = "desc";

            // Act
            var error = TtError.Make(ErrorLevel.Error, category, description);

            // Assert
            error.Level.Should().Be(ErrorLevel.Error);
            error.Category.Should().Be(category);
            error.Description.Should().Be(description);
            error.StackTrace.Should().NotBeEmpty();
        }
    }
}
