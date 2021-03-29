using System;
using System.Linq;
using FluentAssertions;
using TwoTrack.Core.Defaults;
using Xunit;

namespace TwoTrack.Core.UnitTests.TtResultTests
{
    public class DoImmutabilityTests
    {
        private readonly Action _throwAccessViolationException = () => throw new AccessViolationException();
        private readonly Action _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Action _doNothing = () => { };

        [Fact]
        public void Do_NullArgumentError_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = global::TwoTrack.Core.TwoTrack.Ok();
            var result2 = result1.Do(null);

            // Assert
            result1.Succeeded.Should().BeTrue();
            result2.Failed.Should().BeTrue();
            result2.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void Do_ThrowAccessViolationException_ExpectExceptionError()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok()
                .SetExceptionFilter(ex => true)
                .Do(_throwAccessViolationException);

            // Assert
            result.Failed.Should().BeTrue();
            result.Errors.First().Category.Should().Be(ErrorCategory.Exception);
        }

        [Fact]
        public void Do_ThrowArgumentNullException_ExpectUncaughtException()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok()
                .SetExceptionFilter(ex => true)
                .Do(_throwArgumentNullException);

            // Assert
            result.Failed.Should().BeTrue();
            result.Errors.First().Category.Should().Be(ErrorCategory.Exception);
        }

        [Fact]
        public void Do_AllOk_Succeeded()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok().Do(_doNothing);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
    }
}
