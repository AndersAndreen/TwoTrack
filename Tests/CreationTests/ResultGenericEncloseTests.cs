using FluentAssertions;
using Tests.TestHelpers;
using TwoTrackResult;
using Xunit;

namespace Tests.CreationTests
{
    public class ResultGenericEncloseTests
    {
        [Fact]
        public void ResultFactory_EncloseInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Enclose(() => 3);

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void ResultFactory_EncloseTtResultOfInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Enclose(() => TwoTrack.Enclose(()=>3));

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
