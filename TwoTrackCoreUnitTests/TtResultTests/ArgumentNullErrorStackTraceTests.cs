using FluentAssertions;
using System;
using System.Linq;
using TwoTrackCore;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
{
    public class ArgumentNullErrorStackTraceTests
    {

        [Fact]
        public void TtResultArgumentNullErrorMethod_ExpectStackTraceToPointToThisFile()
        {
            // Arrange
            var input = new Func<ITwoTrack>[]
            {
                () => TwoTrack.Ok().SetExceptionFilter(default),
                () => TwoTrack.Ok().AddError(default),
                () => TwoTrack.Ok().AddErrors(default),
                () => TwoTrack.Ok().AddConfirmation(default),
                () => TwoTrack.Ok().AddConfirmations(default),
                () => TwoTrack.Ok().Do((Action)default),
                () => TwoTrack.Ok().Do((Func<ITwoTrack>)default),
                () => TwoTrack.Ok().Do((Func<ITwoTrack<int>>)default),
            };
            string testFileName = new System.Diagnostics.StackTrace(true).GetFrame(0).GetFileName();

            // Act
            var results = input.Select(method => method().Errors.Last().Description).ToList();

            //Assert
            results.ForEach(description => description.Should().Contain(testFileName));
        }






    }
}
