using FluentAssertions;
using System;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultGenericTests
{
    /*
        ## Do(Func<ITwoTrack>)
        step 1 states:
        1: succeeeded - 0 errors (e)
        2: failed - 1 error (e)
        
        step 2 actions and e(e)pected results:
        1: null -> e+1 error
        2: throws -> e+1 error
        3: Error -> e+1 error
        4: succeeded -> if (1 succeeded) e+0 errors
     */
    public class DoFuncITwoTrackRetainConfirmationsTests
    {
        private readonly Func<ITwoTrack> _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Func<ITwoTrack> _fail = () => TwoTrack.Fail(TwoTrackError.DefaultError());
        private readonly Func<ITwoTrack> _succeed = TwoTrack.Ok;

        // 1: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_1_Null_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"));
            var result2 = result1.Do((Func<ITwoTrack>)default);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_ThrowAndCatch_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_FuncFailed_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"));
            var result2 = result1.Do(() => _fail());

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_FuncSucceded_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"));
            var result2 = result1.Do(() => _succeed());

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        // 2: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_2_Null_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError());
            var result2 = result1.Do((Func<ITwoTrack>)default);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_2_ThrowAndCatch_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError()).SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_2_FuncFailedNotRun_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError());
            var result2 = result1.Do(() => _fail());

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_2_FuncSucceededNotRun_ExpectConfirmationsCopied()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1)
                .AddConfirmation(TtConfirmation.Make(ConfirmationLevel.Report, "#", "Message"))
                .AddError(TwoTrackError.DefaultError());
            var result2 = result1.Do(() => _succeed());

            // Assert
            result1.Confirmations.Count.Should().Be(1);
            result2.Confirmations.Count.Should().Be(1);
        }
    }
}
