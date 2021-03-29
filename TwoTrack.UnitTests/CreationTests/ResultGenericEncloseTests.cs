using FluentAssertions;
using TwoTrack.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.UnitTests.CreationTests
{
    public class ResultGenericEncloseTests
    {
        [Fact]
        public void TwoTrack_EncloseInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Core.TwoTrack.Enclose(() => 3);

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void TwoTrack_EncloseTtResultOfInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Core.TwoTrack.Enclose(() => TwoTrack.Core.TwoTrack.Enclose(()=>3));

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
