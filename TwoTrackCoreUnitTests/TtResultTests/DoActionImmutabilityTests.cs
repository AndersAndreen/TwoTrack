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
    public class DoActionImmutabilityTests
    {
        private string StateTestValue = "";
        private readonly Action _throwAccessViolationException = () => throw new AccessViolationException();
        private readonly Action _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Action<DoActionImmutabilityTests> _changeState = (obj) => { obj.StateTestValue = "#"; };

        // 1: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.Do((Action)default);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_ThrowAndCatch_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Exception);
        }

        [Fact]
        public void DoAction_1_Action_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.Do(() => _changeState(this));

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(0);
            StateTestValue.Should().Be("#");
        }


        // 2: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_2_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail();
            var result2 = result1.Do((Action)default);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Unspecified);
        }

        [Fact]
        public void DoAction_2_ThrowAndCatch_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail().SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Unspecified);
        }

        [Fact]
        public void DoAction_2_Action_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail();
            var result2 = result1.Do(() => _changeState(this));

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(1);
            StateTestValue.Should().Be("");
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Unspecified);
        }
    }
}
