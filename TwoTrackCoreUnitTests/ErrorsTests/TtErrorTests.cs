using System;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
using Xunit;

namespace TwoTrackCoreUnitTests.ErrorsTests
{
    public class TtErrorTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("cat", null)]
        [InlineData(null, "desc")]
        public void Make_CategoryAndDescriptionNullChecks_ExpectResultFailed(string category, string description)
        {
            // Arrange

            // Act
            var error = TwoTrackError.Error(ErrorLevel.Warning, category, description);

            // Assert
            error.Level.Should().Be(ErrorLevel.Error);
            error.Category.Should().Be(ErrorCategory.ArgumentNullError);
            error.Description.Should().NotBeEmpty();
            error.StackTrace.Should().NotBeEmpty();
        }

        [Fact]
        public void Make_ErrorWithCatAndDesc_ExpectCategoryAndDescriptionAdded()
        {
            // Arrange
            var category = "cat";
            var description = "desc";

            // Act
            var error = TwoTrackError.Error(ErrorLevel.Error, category, description);

            // Assert
            error.Level.Should().Be(ErrorLevel.Error);
            error.Category.Should().Be(category);
            error.Description.Should().Be(description);
            error.StackTrace.Should().NotBeEmpty();
        }

        [Fact]
        public void MakeFromException_ExpectCategoryDescriptionandStackTraceAdded()
        {
            // Arrange
            Exception exception;
            try
            {
                throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Act
            var error = TwoTrackError.Exception(exception);

            // Assert
            error.Level.Should().Be(ErrorLevel.Error);
            error.Category.Should().Be(ErrorCategory.Exception);
            error.Description.Should().Be($"System.ArgumentNullException: {exception.Message}");
            error.StackTrace.Should().NotBeEmpty();
        }

        [Fact]
        public void ToStringTest()
        {
            // Arrange
            var category = "cat";
            var description = "desc";
            // Act
            var error = TwoTrackError.Error(ErrorLevel.Error, category, description);

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
            var error1 = TwoTrackError.Error(eLevel1, cat1, desc1);
            var error2 = TwoTrackError.Error(eLevel2, cat2, desc2);

            // Act
            var result = (error1 == error2);

            // Assert
            result.Should().BeFalse();
        }
    }
}
