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

        [Fact]
        public void ToStringTest()
        {
            // Arrange
            var category = "cat";
            var description = "desc";
            // Act
            var error = TtError.Make(ErrorLevel.Error, category, description);

            // Assert
            error.ToString().Should().Be($"ErrorLevel:{ErrorLevel.Error}, EventType:{category}, Description:{description}");
        }

        [Theory]
        [InlineData(ErrorLevel.Error, "Cat1", "Desc1", ErrorLevel.Critical, "Cat1", "Desc1")]
        [InlineData(ErrorLevel.Error, "Cat1", "Desc1", ErrorLevel.Error, "Cat2", "Desc1")]
        [InlineData(ErrorLevel.Error, "Cat1", "Desc1", ErrorLevel.Error, "Cat1", "Desc2")]
        [InlineData(ErrorLevel.Error, "Cat1", "Desc1", ErrorLevel.Error, null, "Desc1")]
        [InlineData(ErrorLevel.Error, "Cat1", "Desc1", ErrorLevel.Error, "Cat1", null)]
        [InlineData(ErrorLevel.Error, "Cat1", "Desc1", ErrorLevel.Error, null, null)]
        public void Equality_ExpectResultFailed(ErrorLevel eLevel1, string cat1, string desc1, ErrorLevel eLevel2, string cat2, string desc2)
        {
            // Arrange
            var error1 = TtError.Make(eLevel1, cat1, desc1);
            var error2 = TtError.Make(eLevel2, cat2, desc2);

            // Act
            var result = (error1 == error2);

            // Assert
            result.Should().BeFalse();
        }
    }
}
