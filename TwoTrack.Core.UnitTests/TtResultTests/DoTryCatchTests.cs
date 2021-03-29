using System;
using System.Linq;
using FluentAssertions;
using TwoTrack.Core.Defaults;
using Xunit;

namespace TwoTrack.Core.UnitTests.TtResultTests
{
    public class DoTryCatchTests
    {
        private readonly Action _throwAccessViolationException = () => throw new AccessViolationException();
        private readonly Action _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Func<Exception, bool> _argumentNullCatcher = ex => ex is ArgumentNullException;
        private readonly Action _doNothing = () => { };

        [Fact]
        public void Do_nullArgument_NullArgumentError()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok().Do(null);

            // Assert
            result.Failed.Should().BeTrue();
            result.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void Do_ThrowAccessViolationExceptionWithoutCatcher_ExpectUncaughtExeption()
        {
            // Arrange
            Action act = () => global::TwoTrack.Core.TwoTrack.Ok().Do(_throwAccessViolationException);

            // Act

            // Assert
            act.Should().Throw<AccessViolationException>();
        }

        [Fact]
        public void Do_ThrowsArgumentNullWithoutCatcher_ExpectUncaughtExeption()
        {
            // Arrange
            Action act = () => global::TwoTrack.Core.TwoTrack.Ok().Do(_throwArgumentNullException);

            // Act

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Do_ThrowsArgumentNullWithCatcher_ExpectExceptionError()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok()
                .SetExceptionFilter(_argumentNullCatcher)
                .Do(_throwArgumentNullException);

            // Assert
            result.Failed.Should().BeTrue();
            result.Errors.First().Category.Should().Be(ErrorCategory.Exception);
        }

        [Fact]
        public void Do_ThrowAccessViolationExceptionWithNullExceptionCatcher_ExpectUncaughtExeption()
        {
            // Arrange
            Action act = () => global::TwoTrack.Core.TwoTrack.Ok()
                .SetExceptionFilter(_argumentNullCatcher)
                .Do(_throwAccessViolationException);

            // Act
            // Assert
            act.Should().Throw<AccessViolationException>();
        }

        [Fact]
        public void Do_AllOk_ExpectSucceeded()
        {
            // Arrange
            // Act
            var result = global::TwoTrack.Core.TwoTrack.Ok().Do(_doNothing);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
    }
}
