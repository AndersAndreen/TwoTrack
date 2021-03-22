using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.TestHelpers;
using TwoTrackResult;
using Xunit;

namespace Tests.CreationTests
{
    public class ResultFailTests
    {
        // ----------------------------------------------------------------------------------------
        // Summary of all TwoTrack.Fail() overloads
        // ----------------------------------------------------------------------------------------

        [Fact]
        public void TwoTrack_SummaryOfAllTestsBelow_ExpectFailedTtResults()
        {
            // Arrange
            // Act
            var results = new List<TtResult>
            {
                TwoTrack.Fail(new List<TtError>()),
                TwoTrack.Fail(default(IEnumerable<TtError>)),
                TwoTrack.Fail(default(Exception)),
                TwoTrack.Fail(new ArgumentOutOfRangeException()),
                TwoTrack.Fail(TwoTrack.Fail()),
                TwoTrack.Fail(),
            };

            // Assert
            results.Should().AllBeEquivalentTo(TwoTrack.Fail(), opt => opt.Excluding(res => res.Errors));
            results.ForEach(r => r.Errors.Count.Should().Be(1));
        }

        // ----------------------------------------------------------------------------------------
        // Individual tests
        // ----------------------------------------------------------------------------------------
        [Fact]
        public void TwoTrack_FailWithEmptyTtErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(new List<TtError>());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailWithNullErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(default(IEnumerable<TtError>));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailWithNullAsExceptionArgument_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(default(Exception));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailWithException_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(new ArgumentOutOfRangeException());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Fail();

            // Act
            var result2 = TwoTrack.Fail(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_Fail_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result2 = TwoTrack.Fail();

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }
    }
}
