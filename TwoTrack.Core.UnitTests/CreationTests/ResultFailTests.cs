using System;
using System.Collections.Generic;
using FluentAssertions;
using TwoTrack.Core.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.Core.UnitTests.CreationTests
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
                global::TwoTrack.Core.TwoTrack.Fail(new List<TtError>()),
                global::TwoTrack.Core.TwoTrack.Fail(default(IEnumerable<TtError>)),
                global::TwoTrack.Core.TwoTrack.Fail(default(Exception)),
                global::TwoTrack.Core.TwoTrack.Fail(new ArgumentOutOfRangeException()),
                global::TwoTrack.Core.TwoTrack.Fail(global::TwoTrack.Core.TwoTrack.Fail()),
                global::TwoTrack.Core.TwoTrack.Fail(),
            };

            // Assert
            global::TwoTrack.Core.TwoTrack.Fail().Failed.Should().BeTrue();
            results.Should().AllBeEquivalentTo(global::TwoTrack.Core.TwoTrack.Fail(), opt => opt.Excluding(res => res.Errors).Excluding(res=>res.ExceptionFilter));
            results.ForEach(r => r.Errors.Count.Should().Be(1));
        }

        // ----------------------------------------------------------------------------------------
        // Individual tests
        // ----------------------------------------------------------------------------------------
        [Fact]
        public void TwoTrack_FailWithEmptyTtErrorList_ExpectedFailureStates() 
            => TestAssert(() => global::TwoTrack.Core.TwoTrack.Fail(new List<TtError>()));

        [Fact]
        public void TwoTrack_FailWithNullErrorList_ExpectedFailureStates() 
            => TestAssert(() => global::TwoTrack.Core.TwoTrack.Fail(default(IEnumerable<TtError>)));

        [Fact]
        public void TwoTrack_FailWithNullAsExceptionArgument_ExpectedFailureStates()
            => TestAssert(() => global::TwoTrack.Core.TwoTrack.Fail(default(Exception)));

        [Fact]
        public void TwoTrack_FailWithException_ExpectedFailureStates() 
            => TestAssert(() => global::TwoTrack.Core.TwoTrack.Fail(new ArgumentOutOfRangeException()));

        [Fact]
        public void TwoTrack_Fail_ExpectedFailureStates() 
            => TestAssert(() => global::TwoTrack.Core.TwoTrack.Fail());

        [Fact]
        public void TwoTrack_FailWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = global::TwoTrack.Core.TwoTrack.Fail();

            // Act
            TestAssert(() => global::TwoTrack.Core.TwoTrack.Fail(result1));
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
