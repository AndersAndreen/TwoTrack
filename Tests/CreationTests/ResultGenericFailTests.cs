using FluentAssertions;
using System;
using System.Collections.Generic;
using Tests.TestHelpers;
using TwoTrackResult;
using Xunit;

namespace Tests.CreationTests
{
    public class ResultGenericFailTests
    {
        [Fact]
        public void ResultFactory_FailTWithEmptyTtErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(new List<TtError>());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailTWithNullErrorList_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(default(IEnumerable<TtError>));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailTWithNullAsExceptionArgument_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(default(Exception));

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailTWithException_ExpectedFailureStates()
        {
            // Arrange

            // Act
            var result = TwoTrack.Fail<int>(new ArgumentOutOfRangeException());

            // Assert
            result.AssertBasicAppResultFailureCriteria();
            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailTWithResult_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Ok().AddError(TtError.DefaultError());

            // Act
            var result2 = TwoTrack.Fail<int>(result1);

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void ResultFactory_FailT_ExpectedFailureStates()
        {
            // Arrange
            var result1 = TwoTrack.Ok().AddError(TtError.DefaultError());

            // Act
            var result2 = TwoTrack.Fail<int>();

            // Assert
            result2.AssertBasicAppResultFailureCriteria();
            result2.Errors.Count.Should().Be(1);
        }
    }
}
