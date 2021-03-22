using System;
using System.Linq;
using FluentAssertions;
using TwoTrackResult;
using Xunit;

namespace Tests.TtResultGenericTests
{
    public class EncloseImmutabilityTests
    {
        private readonly Func<int> _throwAccessViolationException = () => throw new AccessViolationException();
        private readonly Func<int> _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Func<int> _returnOne = () => 1;

        [Fact]
        public void Enclose_NullArgumentError_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Enclose(_returnOne);
            var result2 = result1.Do(default(Action));

            // Assert
            result1.Succeeded.Should().BeTrue();
            result2.Failed.Should().BeTrue();
            result2.Errors.First().Category.Should().Be(TwoTrackResult.Defaults.Category.ArgumentNullError);
        }

        //[Fact]
        //public void Enclose_ThrowAccessViolationException_ExpectExceptionError()
        //{
        //    // Arrange
        //    // Act
        //    var result = TwoTrack.Ok()
        //        .SetExceptionFilter(ex => true)
        //        .Do(_throwAccessViolationException);

        //    // Assert
        //    result.Failed.Should().BeTrue();
        //    result.Errors.First().Category.Should().Be(TwoTrackResult.Defaults.Category.Exception);
        //}

        //[Fact]
        //public void Enclose_ThrowArgumentNullException_ExpectUncaughtException()
        //{
        //    // Arrange
        //    // Act
        //    var result = TwoTrack.Ok()
        //        .SetExceptionFilter(ex => true)
        //        .Do(_throwArgumentNullException);

        //    // Assert
        //    result.Failed.Should().BeTrue();
        //    result.Errors.First().Category.Should().Be(TwoTrackResult.Defaults.Category.Exception);
        //}

        //[Fact]
        //public void Enclose_AllOk_Succeeded()
        //{
        //    // Arrange
        //    // Act
        //    var result = TwoTrack.Ok().Do(_returnOne);

        //    // Assert
        //    result.Succeeded.Should().BeTrue();
        //}
    }
}
