using FluentAssertions;
using TwoTrack.Core.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.Core.UnitTests.CreationTests
{
    public class ResultOkTests
    {
        [Fact]
        public void TwoTrack_Ok_InitialStates()
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
