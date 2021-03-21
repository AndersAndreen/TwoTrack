using FluentAssertions;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;
using TwoTrackResult;
using Xunit;

namespace Tests.CreationTests
{
    public class ResultFailTests
    {
        [Fact]
        public void ResultFactory_FailWithEmptyTtErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(new List<TtError>());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailWithNullErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(default(IEnumerable<TtError>));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailWithNullAsExceptionArgument_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(default(Exception));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailWithException_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail(new ArgumentOutOfRangeException());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Ok().AddError(TtError.DefaultError());

            // Act
            var result2 = TwoTrack.Fail(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_Fail_ExpectedFailureStates()
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
