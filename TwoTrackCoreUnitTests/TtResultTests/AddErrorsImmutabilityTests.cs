using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    /*
        ## AddErrors(IEnumerable<TtError>)
        step 1 states:
        1: succeeeded - 0 errors
        2: failed - 1 error
        
        step 2 actions and e(e)pected results (when adding two errors):
        1: null -> e+1 errors 
        3: error -> e+2 errors
        (3 is untestable since TtError is a value object): throws -> e+1 errors 
        
        => 4 tests needed
     */
    public class AddErrorsImmutabilityTests
    {
        private readonly IEnumerable<TtError> _twoErrors = new List<TtError> { TwoTrackError.DefaultError(), TwoTrackError.ArgumentNullError() };

        // 1: --------------------------------------------------------------------------------------------
        [Fact]
        public void AddError_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.AddErrors((IEnumerable<TtError>)null);

            // Assert
            result1.Errors.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(1);
            result2.Errors.Last().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void AddError_1_Error_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.AddErrors(_twoErrors);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(2);
        }


        // 2: --------------------------------------------------------------------------------------------
        [Fact]
        public void AddErrors_2_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail(TwoTrackError.DefaultError());
            var result2 = result1.AddErrors((IEnumerable<TtError>)null);

            // Assert
            result1.Errors.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(2);
            result2.Errors.Last().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void AddErrors_2_Error_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail(TwoTrackError.DefaultError());
            var result2 = result1.AddErrors(_twoErrors);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(3);
        }

    }
}
