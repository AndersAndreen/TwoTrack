﻿using FluentAssertions;
using System.Linq;
using TwoTrackResult;
using TwoTrackResult.Defaults;
using Xunit;

namespace Tests.TtResultTests
{
    public class SetExceptionFilterTests
    {
        [Fact]
        public void SetExceptionFilter_nullArgument_NullArgumentError()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok().SetExceptionFilter(null);

            // Assert
            result.Errors.First().Category.Should().Be(ErrorCategory.ArgumentNullError);
        }

        [Fact]
        public void SetExceptionFilter_ExpectOk()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok().SetExceptionFilter(ex => false);

            // Assert
            result.Succeeded.Should().BeTrue();
        }
    }
}
