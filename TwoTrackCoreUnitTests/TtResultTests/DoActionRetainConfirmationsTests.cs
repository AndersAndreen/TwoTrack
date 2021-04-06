using System;
using System.Linq;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    /*
        ## Do(Action)
        step 1 states:
        1: succeeeded - 0 errors (e)
        2: failed - 1 error (e)
        
        step 2 actions and e(e)pected results:
        1: null -> e+1 errors
        2:  throws -> e+1 errors
        3: succeeded -> if (1 succeeded) e+0 errors
        
        => 6 tests needed
     */
    public class DoActionRetainConfirmationsTests
    {
        private string StateTestValue = "";
        private readonly Action _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Action<DoActionRetainConfirmationsTests> _changeState = (obj) => { obj.StateTestValue = "#"; };

        // 1: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"));
            var result2 = result1.Do((Action)default);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_ThrowAndCatch_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_Action_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"));
            var result2 = result1.Do(() => _changeState(this));

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }


        // 2: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_2_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError());
            var result2 = result1.Do((Action)default);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_2_ThrowAndCatch_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError()).SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_2_Action_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError());
            var result2 = result1.Do(() => _changeState(this));

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }
    }
}
