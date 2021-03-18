using FluentAssertions;
using Tests.TestHelpers;
using TwoTrackResult;
using Xunit;

namespace Tests.Creation
{
    public class ResultOkTests
    {
        [Fact]
        public void ResultFactory_Ok_InitialStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Ok();

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
