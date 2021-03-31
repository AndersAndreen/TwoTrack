using FluentAssertions;
using TwoTrackCoreUnitTests.TestHelpers;
using Xunit;

namespace TwoTrackCoreUnitTests.CreationTests
{
    public class ResultOkTests
    {
        [Fact]
        public void TwoTrack_Ok_InitialStates()
        {
            // Arrange

            // Act
            var result = TwoTrackCore.TwoTrack.Ok();

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
