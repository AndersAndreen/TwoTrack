using System;
using FluentAssertions;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultGenericTests
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
            var result1 = TwoTrackCore.TwoTrack.Enclose(_returnOne);
            var result2 = result1.Do(x => default(Action));

            // Assert
            result1.Succeeded.Should().BeTrue();
            result2.Failed.Should().BeTrue();
        }


    }
}
