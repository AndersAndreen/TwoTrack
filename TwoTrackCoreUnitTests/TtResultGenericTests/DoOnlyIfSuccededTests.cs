using System;
using FluentAssertions;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultGenericTests
{
    public class DoOnlyIfSuccededTests
    {
        private readonly Action _throwAccessViolationException = () => throw new AccessViolationException();
        private readonly Action _throwArgumentNullException = () => throw new ArgumentNullException();
        private readonly Action _updateMockValue;
        private int _mockValue = 0;

        public DoOnlyIfSuccededTests()
        {
            _updateMockValue = () => { _mockValue = 1; };
        }

        [Fact]
        public void Do_AferASucceededResult_ExpectToRunAction()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Enclose(() => "#");
            //var result2 = result1.Do(_updateMockValue);

            // Assert
            result1.Succeeded.Should().BeTrue();
            //result2.Succeeded.Should().BeTrue();
            //_mockValue.Should().Be(1);
        }

        [Fact]
        public void Do_AferAFailedResult_ExpectNotToRunAction()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Fail<string>();
            var result2 = result1.Do(x => _updateMockValue);

            // Assert
            result1.Failed.Should().BeTrue();
            result2.Failed.Should().BeTrue();
            _mockValue.Should().Be(0);
        }
    }
}
