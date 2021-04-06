using System;
using FluentAssertions;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    public class DoOnlyIfSuccededTests
    {
        private readonly Action _storeOne;
        private int _mockValue = 0;

        public DoOnlyIfSuccededTests()
        {
            _storeOne = () => { _mockValue = 1; };
        }

        [Fact]
        public void Do_AferASucceededResult_ExpectToRunAction()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok();
            var result2 = result1.Do(_storeOne);

            // Assert
            result1.Succeeded.Should().BeTrue();
            result2.Succeeded.Should().BeTrue();
            _mockValue.Should().Be(1);
        }

        [Fact]
        public void Do_AferAFailedResult_ExpectNotToRunAction()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail(TwoTrackError.DefaultError());
            var result2 = result1.Do(_storeOne);

            // Assert
            result1.Failed.Should().BeTrue();
            result2.Failed.Should().BeTrue();
            _mockValue.Should().Be(0);
        }
    }
}
