using System;
using FluentAssertions;
using Xunit;
using TwoTrackCore.Defaults;

namespace TwoTrack.Core.UseCaseScenarioTests
{
    public class UnitTest1
    {
        [Fact]
        public void DoAction_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var result1 = TwoTrackCore.TwoTrack.Ok();
            var result2 = result1.Do((Action)default);

            // Assert
            result1.Errors.Count.Should().Be(0);
            result2.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void Test1()
        {
            var x = TwoTrackCore.TwoTrack.Ok();

        }
    }
}
