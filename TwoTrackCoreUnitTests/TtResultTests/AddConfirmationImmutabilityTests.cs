using System;
using FluentAssertions;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    /*
        ## AddConfirmation(TtConfirmation)
        step 1 states:
        1: succeeeded - 0 errors (e), 0 confirmations (c)
        2: failed - 1 error (e), 0 confirmations (c)
        3: succeeeded - 0 errors (e), 1 confirmations (c)
        4: failed - 1 error (e), 1 confirmations (c)
        
        step 2 actions and e(e)pected results:
        1: null -> e+1 error, c+0 confirmations
        2: confirmation -> e+0 error, c+1 confirmation
        (3 is untestable since TtError is a value object): throws -> e+1 error, c+0 confirmations
        
        => 8 tests needed
     */
    public class AddConfirmationImmutabilityTests
    {
        private readonly Action _throwAccessViolationException = () => throw new AccessViolationException();
        private readonly Action _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Action _doNothing = () => { };

        private readonly TtConfirmation _confirmation1 = TtConfirmation.Make(ConfirmationLevel.Report, "cat", "miaow");
        private readonly TtConfirmation _confirmation2 = TtConfirmation.Make(ConfirmationLevel.Report, "dog", "woof");
        // 1
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmation_1_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok();
            var result2 = result1.AddConfirmation((TtConfirmation)null);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(0);
        }

        [Fact]
        public void AddConfirmation_1_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok();
            var result2 = result1.AddConfirmation(_confirmation1);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(0);
            result2.Confirmations.Count.Should().Be(1);
        }


        // 2
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmation_2_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Fail();
            var result2 = result1.AddConfirmation((TtConfirmation)null);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(2);
            result2.Confirmations.Count.Should().Be(0);
        }

        [Fact]
        public void AddConfirmation_2_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Fail();
            var result2 = result1.AddConfirmation(_confirmation1);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(0);
        }


        // 3
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmation_3_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok().AddConfirmation(_confirmation1);
            var result2 = result1.AddConfirmation((TtConfirmation)null);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void AddConfirmation_3_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok().AddConfirmation(_confirmation1);
            var result2 = result1.AddConfirmation(_confirmation2);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(0);
            result2.Confirmations.Count.Should().Be(2);
        }


        // 3
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmation_4_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok().AddConfirmation(_confirmation1).AddError(TtError.DefaultError());
            var result2 = result1.AddConfirmation((TtConfirmation)null);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(2);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void AddConfirmation_4_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok().AddConfirmation(_confirmation1).AddError(TtError.DefaultError());
            var result2 = result1.AddConfirmation(_confirmation2);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }
    }
}
