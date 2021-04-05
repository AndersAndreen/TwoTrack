using System;
using FluentAssertions;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultGenericTests
{
    public class SelectOnlyIfSuccededTests
    {
        private readonly Func<int,int> _storeMock;
        private int _mockValue = 0;

        public SelectOnlyIfSuccededTests()
        {
            _storeMock = inp =>
            {
                _mockValue = inp;
                return inp;
            };
        }

        [Fact]
        public void Do_AferASucceededResult_ExpectToRunAction()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Enclose(() => 1);

            // Assert
            _ = result1.Do(_storeMock);
            result1.Succeeded.Should().BeTrue();
            _mockValue.Should().Be(1);
        }

        [Fact]
        public void Do_AferAFailedResult_ExpectNotToRunAction()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Fail<int>(TwoTrackError.DefaultError());
            var result2 = result1.Do(_storeMock);

            // Assert
            result1.Failed.Should().BeTrue();
            result2.Failed.Should().BeTrue();
            _mockValue.Should().Be(0);
        }
    }
}
