using FluentAssertions;
using TwoTrackCore;
using TwoTrackCoreUnitTests.TestHelpers;
using Xunit;

namespace TwoTrackCoreUnitTests.CreationTests
{
    public class ResultGenericEncloseTests
    {
        [Fact]
        public void TwoTrack_EncloseInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Ok().Enclose(() => 3);

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void TwoTrack_EncloseTtResultOfInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Enclose(() => TwoTrack.Ok().Enclose(()=>3));

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
