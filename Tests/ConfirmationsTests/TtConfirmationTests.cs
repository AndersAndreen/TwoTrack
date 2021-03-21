using System;
using FluentAssertions;
using TwoTrackResult;
using TwoTrackResult.Defaults;
using Xunit;

namespace Tests.ConfirmationsTests
{
    public class TtConfirmationTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData("cat", null)]
        [InlineData(null, "desc")]
        public void Make_CategoryAndDescriptionNullChecks_ExpectUncaughtExeption(string category, string description)
        {
            // Arrange

            // Act
            Action act = () => TtConfirmation.Make(ConfirmationLevel.Debug, category, description);

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Make_CatAndDescGiven_ExpectCategoryAndDescriptionAdded()
        {
            // Arrange
            var category = "cat";
            var description = "desc";

            // Act
            var confirmation = TtConfirmation.Make(ConfirmationLevel.Debug, category, description);

            // Assert
            confirmation.Level.Should().Be(ConfirmationLevel.Debug);
            confirmation.Category.Should().Be(category);
            confirmation.Description.Should().Be(description);
        }

        [Fact]
        public void ToStringTest()
        {
            // Arrange
            var category = "cat";
            var description = "desc";
            // Act
            var error = TtConfirmation.Make(ConfirmationLevel.Debug, category, description);

            // Assert
            error.ToString().Should().Be($"ErrorLevel:{ConfirmationLevel.Debug}, EventType:{category}, Description:{description}");
        }

        [Theory]
        [InlineData(ConfirmationLevel.Debug, "Cat1", "Desc1", ConfirmationLevel.Information, "Cat1", "Desc1")]
        [InlineData(ConfirmationLevel.Debug, "Cat1", "Desc1", ConfirmationLevel.Debug, "Cat2", "Desc1")]
        [InlineData(ConfirmationLevel.Debug, "Cat1", "Desc1", ConfirmationLevel.Debug, "Cat1", "Desc2")]
        public void Equality_ExpectResultFailed(ConfirmationLevel eLevel1, string cat1, string desc1, ConfirmationLevel eLevel2, string cat2, string desc2)
        {
            // Arrange
            var confirmation1 = TtConfirmation.Make(eLevel1, cat1, desc1);
            var confirmation2 = TtConfirmation.Make(eLevel2, cat2, desc2);

            // Act
            var result = (confirmation1 == confirmation2);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData(ConfirmationLevel.Debug, "Cat1", "Desc1", ConfirmationLevel.Debug, "Cat1", "Desc1")]
        [InlineData(ConfirmationLevel.Information, "Cat1", "Desc1", ConfirmationLevel.Information, "Cat1", "Desc1")]
        [InlineData(ConfirmationLevel.Report, "Cat1", "Desc1", ConfirmationLevel.Report, "Cat1", "Desc1")]
        public void Equality_ExpectResultSucceeded(ConfirmationLevel eLevel1, string cat1, string desc1, ConfirmationLevel eLevel2, string cat2, string desc2)
        {
            // Arrange
            var confirmation1 = TtConfirmation.Make(eLevel1, cat1, desc1);
            var confirmation2 = TtConfirmation.Make(eLevel2, cat2, desc2);

            // Act
            var result = (confirmation1 == confirmation2);

            // Assert
            result.Should().BeTrue();
        }
    }
}
