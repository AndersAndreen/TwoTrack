using FluentAssertions;
using Tests.TestHelpers;
using TwoTrackResult;
using Xunit;

namespace Tests.Creation
{
    public class ResultFailTests
    {
        [Fact]
        public void ResultFactory_Fail_InitialStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail();

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }
    }
}
