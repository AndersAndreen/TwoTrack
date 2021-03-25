using System;
using System.Linq;
using FluentAssertions;
using TwoTrackResult;
using TwoTrackResult.Defaults;
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
            result2.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }


    }
}
