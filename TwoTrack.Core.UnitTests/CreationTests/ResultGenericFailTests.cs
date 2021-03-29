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
                global::TwoTrack.Core.TwoTrack.Fail<int>(new List<TtError>()),
                global::TwoTrack.Core.TwoTrack.Fail<int>(default(IEnumerable<TtError>)),
                global::TwoTrack.Core.TwoTrack.Fail<int>(default(Exception)),
                global::TwoTrack.Core.TwoTrack.Fail<int>(new ArgumentOutOfRangeException()),
                global::TwoTrack.Core.TwoTrack.Fail<int>(global::TwoTrack.Core.TwoTrack.Enclose(()=>1).AddError(TtError.DefaultError())),
                global::TwoTrack.Core.TwoTrack.Fail<int>(global::TwoTrack.Core.TwoTrack.Fail()),
                global::TwoTrack.Core.TwoTrack.Fail<int>(),
            };

            // Assert
            global::TwoTrack.Core.TwoTrack.Fail().Failed.Should().BeTrue();
            results.Should().AllBeEquivalentTo(global::TwoTrack.Core.TwoTrack.Fail(), 
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
            var result = global::TwoTrack.Core.TwoTrack.Fail<int>(new List<TtError>());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithNullErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = global::TwoTrack.Core.TwoTrack.Fail<int>(default(IEnumerable<TtError>));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithNullAsExceptionArgument_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = global::TwoTrack.Core.TwoTrack.Fail<int>(default(Exception));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithException_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = global::TwoTrack.Core.TwoTrack.Fail<int>(new ArgumentOutOfRangeException());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithResultOfT_ExpectedFailureStates()
        {
            // Arrange
            var result1 = global::TwoTrack.Core.TwoTrack.Enclose(() => 1).AddError(TtError.DefaultError());

            // Act
            var result2 = global::TwoTrack.Core.TwoTrack.Fail<int>(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailTWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = global::TwoTrack.Core.TwoTrack.Fail();

            // Act
            var result2 = global::TwoTrack.Core.TwoTrack.Fail<int>(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void TwoTrack_FailT_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result2 = global::TwoTrack.Core.TwoTrack.Fail<int>();

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }
    }
}
