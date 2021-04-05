using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
using Xunit;

namespace TwoTrackCoreUnitTests.ErrorsTests
{
    public class TtResultAddErrorsTests
    {
        #region AddErrors(TtResult)
        [Fact]
        public void AddNullToSucceded_TtResult_ExpectArgumentNullError()
        {
            // Arrange

            // Act
            var result = TwoTrack.Ok().AddError(default);

            // Assert
            result.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void AddNullToSucceded_TtResult_ExpectImmutability()
        {
            // Arrange
            var result = TwoTrack.Ok();
            var result2 = TwoTrack.Fail(TwoTrackError.DefaultError()).AddError(default);

            // Act
            var result3 = result.AddErrors(result2.Errors);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
            result3.Succeeded.Should().BeFalse();
            result3.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void AddErrorsToSucceded_TtResult_ExpectImmutability()
        {
            // Arrange
            var error = TwoTrackError.Error(ErrorLevel.Warning);
            var result = TwoTrack.Ok();
            var result2 = TwoTrack.Fail(TwoTrackError.DefaultError()).AddError(error);

            // Act
            var result3 = result.AddErrors(result2.Errors);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
            result3.Succeeded.Should().BeFalse();
            result3.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void AddErrorsToSucceded_TtResultSucceeded_ExpectResultSucceded()
        {
            // Arrange

            // Act
            var result1 = TwoTrack.Ok();
            var result2 = TwoTrack.Ok().AddErrors(result1.Errors);

            // Assert
            result2.Succeeded.Should().BeTrue();
        }
        #endregion


        #region AddErrors(IEnumerable<TtError>)
        [Fact]
        public void AddNullToSucceded_IenumerableOfTtError_ExpectArgumentNullError()
        {
            // Arrange

            // Act
            var result = TwoTrack.Ok().AddErrors(default(IEnumerable<TtError>));

            // Assert
            result.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void AddNullToSucceded_IenumerableOfTtError_ExpectImmutability()
        {
            // Arrange
            var result = TwoTrack.Ok();
            var result2 = TwoTrack.Fail(TwoTrackError.DefaultError()).AddErrors(default(IEnumerable<TtError>));

            // Act
            var result3 = result.AddErrors(result2.Errors);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
            result3.Succeeded.Should().BeFalse();
            result3.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void AddErrorsToSucceded_IenumerableOfTtError_ExpectImmutability()
        {
            // Arrange
            var error = TwoTrackError.Error(ErrorLevel.Warning);
            var result = TwoTrack.Ok();
            var result2 = TwoTrack.Fail(TwoTrackError.DefaultError()).AddError(error);

            // Act
            var result3 = result.AddErrors(result2.Errors);

            // Assert
            result.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeFalse();
            result3.Succeeded.Should().BeFalse();
            result3.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void AddErrorsToSucceded_EmptyErrorList_ExpectResultSucceded()
        {
            // Arrange

            // Act
            var result = TwoTrack.Ok().AddErrors(new List<TtError>());

            // Assert
            result.Succeeded.Should().BeTrue();
        }
        #endregion



    }
}
