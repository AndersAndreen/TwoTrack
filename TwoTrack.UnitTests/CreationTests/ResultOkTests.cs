using FluentAssertions;
using TwoTrack.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.UnitTests.CreationTests
{
    public class ResultOkTests
    {
        [Fact]
        public void TwoTrack_Ok_InitialStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Core.TwoTrack.Ok();

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
