using System;
using System.Linq;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
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
    public class DoFuncITwoTrackImmutabilityTests
    {
        private readonly Func<ITwoTrack> _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Func<ITwoTrack> _fail = ()=> TwoTrack.Fail(TwoTrackError.DefaultError());
        private readonly Func<ITwoTrack> _succeed = TwoTrack.Ok;

        // 1: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(()=>1);
            var result2 = result1.Do((Func<ITwoTrack<int>>)default);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_ThrowAndCatch_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1).SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Exception);
        }

        [Fact]
        public void DoAction_1_FuncFailed_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1);
            var result2 = result1.Do(() =>_fail());

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void DoAction_1_FuncSucceded_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok().Enclose(() => 1);
            var result2 = result1.Do(() => _succeed());

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(0);
        }

        // 2: --------------------------------------------------------------------------------------------
        [Fact]
        public void DoAction_2_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail<int>(TwoTrackError.DefaultError());
            var result2 = result1.Do((Func<ITwoTrack<int>>)default);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result1.Errors.First().Category.Should().Be(ErrorCategory.Unspecified);
            result2.Errors.Count.Should().Be(2);
            result2.Errors.First().Category.Should().Be(ErrorCategory.Unspecified);
            result2.Errors.Last().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void DoAction_2_ThrowAndCatch_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail<int>(TwoTrackError.DefaultError()).SetExceptionFilter(ex => ex is ArgumentNullException);
            var result2 = result1.Do(_throwArgumentNullException);

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Unspecified);
        }

        [Fact]
        public void DoAction_2_FuncFailedNotRun_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail<int>(TwoTrackError.DefaultError());
            var result2 = result1.Do(() => _fail());

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Unspecified);
        }

        [Fact]
        public void DoAction_2_FuncSucceededNotRun_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail<int>(TwoTrackError.DefaultError());
            var result2 = result1.Do(() => _succeed());

            // Assert
            result1.Errors.Count.Should().Be(1);
            result2.Errors.Count.Should().Be(1);
            result2.Errors.Single().Category.Should().Be(ErrorCategory.Unspecified);
        }
    }
}
