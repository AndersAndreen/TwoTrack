using System;
using System.Collections.Generic;
using FluentAssertions;
using TwoTrack.Core;
using TwoTrack.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.UnitTests.CreationTests
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
                TwoTrack.Core.TwoTrack.Fail(new List<TtError>()),
                TwoTrack.Core.TwoTrack.Fail(default(IEnumerable<TtError>)),
                TwoTrack.Core.TwoTrack.Fail(default(Exception)),
                TwoTrack.Core.TwoTrack.Fail(new ArgumentOutOfRangeException()),
                TwoTrack.Core.TwoTrack.Fail(TwoTrack.Core.TwoTrack.Fail()),
                TwoTrack.Core.TwoTrack.Fail(),
            };

            // Assert
            TwoTrack.Core.TwoTrack.Fail().Failed.Should().BeTrue();
            results.Should().AllBeEquivalentTo(TwoTrack.Core.TwoTrack.Fail(), opt => opt.Excluding(res => res.Errors).Excluding(res=>res.ExceptionFilter));
            results.ForEach(r => r.Errors.Count.Should().Be(1));
        }

        // ----------------------------------------------------------------------------------------
        // Individual tests
        // ----------------------------------------------------------------------------------------
        [Fact]
        public void TwoTrack_FailWithEmptyTtErrorList_ExpectedFailureStates() 
            => TestAssert(() => TwoTrack.Core.TwoTrack.Fail(new List<TtError>()));

        [Fact]
        public void TwoTrack_FailWithNullErrorList_ExpectedFailureStates() 
            => TestAssert(() => TwoTrack.Core.TwoTrack.Fail(default(IEnumerable<TtError>)));

        [Fact]
        public void TwoTrack_FailWithNullAsExceptionArgument_ExpectedFailureStates()
            => TestAssert(() => TwoTrack.Core.TwoTrack.Fail(default(Exception)));

        [Fact]
        public void TwoTrack_FailWithException_ExpectedFailureStates() 
            => TestAssert(() => TwoTrack.Core.TwoTrack.Fail(new ArgumentOutOfRangeException()));

        [Fact]
        public void TwoTrack_Fail_ExpectedFailureStates() 
            => TestAssert(() => TwoTrack.Core.TwoTrack.Fail());

        [Fact]
        public void TwoTrack_FailWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Core.TwoTrack.Fail();

            // Act
            TestAssert(() => TwoTrack.Core.TwoTrack.Fail(result1));
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
