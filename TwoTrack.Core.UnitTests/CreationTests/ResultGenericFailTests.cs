using System;
using System.Collections.Generic;
using FluentAssertions;
using TwoTrack.Core.UnitTests.TestHelpers;
using Xunit;

namespace TwoTrack.Core.UnitTests.CreationTests
{
    public class ResultGenericFailTests
    {
        // ----------------------------------------------------------------------------------------
        // Summary
        // ----------------------------------------------------------------------------------------

        [Fact]
        public void TwoTrack_SummaryOfAllTestsBelow_ExpectFailedTtResults()
        {
            // Arrange
            // Act
            var results = new List<ITwoTrack<int>>
            {
                TwoTrack.Fail<int>(new List<TtError>()),
                TwoTrack.Fail<int>(default(IEnumerable<TtError>)),
                TwoTrack.Fail<int>(default(Exception)),
                TwoTrack.Fail<int>(new ArgumentOutOfRangeException()),
                TwoTrack.Fail<int>(TwoTrack.Enclose(()=>1).AddError(TtError.DefaultError())),
                TwoTrack.Fail<int>(TwoTrack.Fail()),
                TwoTrack.Fail<int>(),
            };

            // Assert
            TwoTrack.Fail().Failed.Should().BeTrue();
            results.Should().AllBeEquivalentTo(TwoTrack.Fail(), 
                opt => opt.Excluding(res => res.Errors).Excluding(res => res.ExceptionFilter));
            results.ForEach(r => r.Errors.Count.Should().Be(1));
        }

        // ----------------------------------------------------------------------------------------
        // Individual tests
        // ----------------------------------------------------------------------------------------

        [Fact]
        public void TwoTrack_FailTWithEmptyTtErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(new List<TtError>());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithNullErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(default(IEnumerable<TtError>));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithNullAsExceptionArgument_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(default(Exception));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithException_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(new ArgumentOutOfRangeException());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithResultOfT_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Enclose(() => 1).AddError(TtError.DefaultError());

            // Act
            var result2 = TwoTrack.Fail<int>(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Fail();

            // Act
            var result2 = TwoTrack.Fail<int>(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailT_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result2 = TwoTrack.Fail<int>();

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }
    }
}
