using System;
using System.Linq;
using FluentAssertions;
using Tests.TestHelpers;
using TwoTrack;
using Xunit;

namespace Tests.Creation
{
    public class ResultFailTests
    {
        [Fact]
        public void ResultFactory_Fail_InitialStates()
        {
            // Arrange

            // Act
            var result = MakeResult.Fail();

            // Assert
            result.Succeeded.Should().BeFalse();
            result.Failed.Should().BeTrue();
            result.Errors.Count.Should().Be(1);
        }
    }
}
