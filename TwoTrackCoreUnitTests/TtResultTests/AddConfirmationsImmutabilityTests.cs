using System;
using System.Collections.Generic;
using FluentAssertions;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    /*
        ## AddConfirmations(IEnumerable<TtConfirmation>)
        step 1 states:
        1: succeeeded - 0 errors (e), 0 confirmations (c)
        2: failed - 1 error (e), 0 confirmations (c)
        3: succeeeded - 0 errors (e), 1 confirmations (c)
        4: failed - 1 error (e), 1 confirmations (c)
        
        step 2 actions and e(e)pected results:
        1: null -> e+1 error, c+0 confirmations
        2: confirmation -> e+0 error, c+2 confirmations
        (3 is untestable since TtError is a value object): throws -> e+1 error, c+0 confirmations
        
        => 8 tests needed
     */
    public class AddConfirmationsImmutabilityTests
    {
        private readonly Action _doNothing = () => { };

        private static readonly TtConfirmation Confirmation1 = TtConfirmation.Make(ConfirmationLevel.Report, "cat", "miaow");
        private static readonly TtConfirmation Confirmation2 = TtConfirmation.Make(ConfirmationLevel.Report, "dog", "woof");
        private static readonly IEnumerable<TtConfirmation> TwoConfirmations = new List<TtConfirmation>{Confirmation1, Confirmation2};
        // 1
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmations_1_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.AddConfirmations((IEnumerable<TtConfirmation>)null);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(0);
        }

        [Fact]
        public void AddConfirmations_1_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.AddConfirmations(TwoConfirmations);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(0);
            result2.Confirmations.Count.Should().Be(2);
        }


        // 2
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmations_2_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail();
            var result2 = result1.AddConfirmations((IEnumerable<TtConfirmation>)null);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(2);
            result2.Confirmations.Count.Should().Be(0);
        }

        [Fact]
        public void AddConfirmations_2_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail();
            var result2 = result1.AddConfirmations(TwoConfirmations);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(0);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(0);
        }


        // 3
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmations_3_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().AddConfirmation(Confirmation1);
            var result2 = result1.AddConfirmations((IEnumerable<TtConfirmation>)null);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void AddConfirmations_3_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().AddConfirmation(Confirmation1);
            var result2 = result1.AddConfirmations(TwoConfirmations);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(0);
            result2.Confirmations.Count.Should().Be(3);
        }


        // 3
        // --------------------------------------------------------------------------------------------
        [Fact]
        public void AddConfirmations_4_null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().AddConfirmation(Confirmation1).AddError(TtError.DefaultError());
            var result2 = result1.AddConfirmations((IEnumerable<TtConfirmation>)null);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(2);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void AddConfirmations_4_confirmation_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().AddConfirmation(Confirmation1).AddError(TtError.DefaultError());
            var result2 = result1.AddConfirmations(TwoConfirmations);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Confirmations.Count.Should().Be(1);

            result2.Errors.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }
    }
}
