using System;
using FluentAssertions;
using Tests.TestHelpers;
using TwoTrack;
using Xunit;

namespace Tests.Creation
{
    public class ResultOkTests
    {
        [Fact]
        public void ResultFactory_Ok_InitialStates()
        {
            // Arrange

            // Act
            var result = MakeResult.Ok();

            // Assert
            result.Succeeded.Should().BeTrue();
            result.Failed.Should().BeFalse();
            result.Errors.Should().BeEmpty();
        }
    }
}
