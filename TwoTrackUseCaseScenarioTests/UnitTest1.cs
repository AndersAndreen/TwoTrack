using System;
using System.Text;
using FluentAssertions;
using TwoTrackCore;
using Xunit;
using TwoTrackCore.Defaults;

namespace TwoTrackUseCaseScenarioTests
{
    public class UnitTest1
    {
        [Fact]
        public void DoAction_1_Null_ExpectImmutability()
        {
            // Arrange
            // Act
            var finalValue = "";
            var result1 = TwoTrack.Enclose(() => 3)
                .Select(nr => new StringBuilder().Insert(0, "bla.", nr).ToString())
                .Select(value=>value.ToUpper())
                .Do(value => finalValue = value);
            var x = "abc";
            // Assert
            finalValue.Should().Be("BLA.BLA.BLA.");
        }

        [Fact]
        public void Test1()
        {
            var x = TwoTrackCore.TwoTrack.Ok();

        }
    }
}
