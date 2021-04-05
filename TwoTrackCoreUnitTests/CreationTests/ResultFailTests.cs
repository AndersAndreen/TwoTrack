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
                TwoTrack.Fail(default(Exception)),
                TwoTrack.Fail(new ArgumentOutOfRangeException()),
                TwoTrack.Fail(TwoTrack.Fail(TwoTrackError.DefaultError())),
                TwoTrack.Fail(TwoTrackError.DefaultError()),
            };

            // Assert
            TwoTrack.Fail(TwoTrackError.DefaultError()).Failed.Should().BeTrue();
            results.Should().AllBeEquivalentTo(TwoTrack.Fail(TwoTrackError.DefaultError()), opt => opt.Excluding(res => res.Errors).Excluding(res=>res.ExceptionFilter));
            results.ForEach(r => r.Errors.Count.Should().Be(1));
        }

        // ----------------------------------------------------------------------------------------
        // Individual tests
        // ----------------------------------------------------------------------------------------
        [Fact]
        public void TwoTrack_FailWithNullAsExceptionArgument_ExpectedFailureStates()
            => TestAssert(() => TwoTrack.Fail(default(Exception)));

        [Fact]
        public void TwoTrack_FailWithException_ExpectedFailureStates() 
            => TestAssert(() => TwoTrack.Fail(new ArgumentOutOfRangeException()));

        [Fact]
        public void TwoTrack_Fail_ExpectedFailureStates() 
            => TestAssert(() => TwoTrack.Fail(TwoTrackError.DefaultError()));

        [Fact]
        public void TwoTrack_FailWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Fail(TwoTrackError.DefaultError());

            // Act
            TestAssert(() => TwoTrack.Fail(result1));
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
