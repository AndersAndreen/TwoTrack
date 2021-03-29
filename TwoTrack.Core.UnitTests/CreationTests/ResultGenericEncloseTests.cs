using FluentAssertions;
using TwoTrack.Core.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.Core.UnitTests.CreationTests
{
    public class ResultGenericEncloseTests
    {
        [Fact]
        public void TwoTrack_EncloseInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = global::TwoTrack.Core.TwoTrack.Enclose(() => 3);

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void TwoTrack_EncloseTtResultOfInt_ExpectSuccessStates()
        {
            // Arrange

            // Act
            var result = global::TwoTrack.Core.TwoTrack.Enclose(() => global::TwoTrack.Core.TwoTrack.Enclose(()=>3));

            // Assert
            result.AssertBasicSuccessCriteria();
            result.Errors.Should().BeEmpty();
        }
    }
}
