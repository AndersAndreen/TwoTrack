using System;
using System.Collections.Generic;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCoreUnitTests.TestHelpers;
using Xunit;

namespace TwoTrackCoreUnitTests.CreationTests
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
            var results = new List<ITwoTrack>
            {
                TwoTrackCore.TwoTrack.Fail(new List<TtError>()),
                TwoTrackCore.TwoTrack.Fail(default(IEnumerable<TtError>)),
                TwoTrackCore.TwoTrack.Fail(default(Exception)),
                TwoTrackCore.TwoTrack.Fail(new ArgumentOutOfRangeException()),
                TwoTrackCore.TwoTrack.Fail(TwoTrackCore.TwoTrack.Fail()),
                TwoTrackCore.TwoTrack.Fail(),
            };

            // Assert
            TwoTrackCore.TwoTrack.Fail().Failed.Should().BeTrue();
            results.Should().AllBeEquivalentTo(TwoTrackCore.TwoTrack.Fail(), opt => opt.Excluding(res => res.Errors).Excluding(res=>res.ExceptionFilter));
            results.ForEach(r => r.Errors.Count.Should().Be(1));
        }

        // ----------------------------------------------------------------------------------------
        // Individual tests
        // ----------------------------------------------------------------------------------------
        [Fact]
        public void TwoTrack_FailWithEmptyTtErrorList_ExpectedFailureStates() 
            => TestAssert(() => TwoTrackCore.TwoTrack.Fail(new List<TtError>()));

        [Fact]
        public void TwoTrack_FailWithNullErrorList_ExpectedFailureStates() 
            => TestAssert(() => TwoTrackCore.TwoTrack.Fail(default(IEnumerable<TtError>)));

        [Fact]
        public void TwoTrack_FailWithNullAsExceptionArgument_ExpectedFailureStates()
            => TestAssert(() => TwoTrackCore.TwoTrack.Fail(default(Exception)));

        [Fact]
        public void TwoTrack_FailWithException_ExpectedFailureStates() 
            => TestAssert(() => TwoTrackCore.TwoTrack.Fail(new ArgumentOutOfRangeException()));

        [Fact]
        public void TwoTrack_Fail_ExpectedFailureStates() 
            => TestAssert(() => TwoTrackCore.TwoTrack.Fail());

        [Fact]
        public void TwoTrack_FailWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrackCore.TwoTrack.Fail();

            // Act
            TestAssert(() => TwoTrackCore.TwoTrack.Fail(result1));
        }

        private void TestAssert(Func<ITwoTrack> func)
        {
            // Act
            var result = func();

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }
    }
}
