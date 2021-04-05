using FluentAssertions;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    /*
        ## AddError(TtError)
        step 1 states:
        1: succeeeded - 0 errors (e)
        2: failed - 1 error (e)
        
        step 2 actions and e(e)pected results:
        1: null -> e+1 errors 
        2: error -> e+1 errors
        (3 is untestable since TtError is a value object): throws -> e+1 errors 
        
        => 4 tests needed
     */
    public class AddErrorImmutabilityTests
    {
        // 1: --------------------------------------------------------------------------------------------
        [Fact]
        public void AddError_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.AddError((TwoTrackError)null);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void AddError_1_Error_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.AddError(TwoTrackError.DefaultError());

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
        }


        // 2: --------------------------------------------------------------------------------------------
        [Fact]
        public void AddErrors_2_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail(TwoTrackError.DefaultError());
            var result2 = result1.AddError((TwoTrackError)null);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void AddErrors_2_Error_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail(TwoTrackError.DefaultError());
            var result2 = result1.AddError(TwoTrackError.DefaultError());

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(2);
        }

    }
}
