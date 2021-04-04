using System;
using System.Linq;
using FluentAssertions;
using TwoTrackCore;
using TwoTrackCore.Defaults;
using Xunit;

namespace TwoTrackCoreUnitTests.TtResultTests
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
            result.Errors.Single().Category.Should().Be(ErrorCategory.ArgumentNullError);
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

        [Fact]
        public void SetExceptionFilter_Default_DoNotCatchAnything()
        {
            // Arrange
            // Act
            Action act = () => TwoTrack.Ok().Do(() => throw new ArgumentNullException());

            // Assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetExceptionFilter_false_CatchNoExceptions()
        {
            // Arrange
            // Act
            Action act = () => TwoTrack.Ok()
                .SetExceptionFilter(ex => false)
                .Do(() => throw new Exception());

            // Assert
            act.Should().Throw<Exception>();
        }

        [Fact]
        public void SetExceptionFilter_true_CatchAllExceptions()
        {
            // Arrange
            // Act
            var result = TwoTrack.Ok()
                .SetExceptionFilter(ex => true)
                .Do(() => throw new Exception());

            // Assert
            result.Errors.Single().Category.Should().Be(ErrorCategory.Exception);
        }

        [Fact]
        public void SetExceptionFilter_CatchListedExceptions()
        {
            // Arrange
            // Act
            var result1 = TwoTrack.Ok()
                .SetExceptionFilter(ex => ex is ArgumentNullException || ex is ApplicationException)
                .Do(() => throw new ArgumentNullException());

            var result2 = TwoTrack.Ok()
                .SetExceptionFilter(ex => ex is ArgumentNullException || ex is ApplicationException)
                .Do(() => throw new ApplicationException());

            // Assert
            result1.Errors.Single().Category.Should().Be(ErrorCategory.Exception);
            result1.Errors.Single().Description.Should().Contain(nameof(ArgumentNullException));

            result2.Errors.Single().Category.Should().Be(ErrorCategory.Exception);
            result2.Errors.Single().Description.Should().Contain(nameof(ApplicationException));
        }

        [Fact]
        public void SetExceptionFilter_DoNotCatchUnListedExceptions()
        {
            // Arrange
            // Act
            Action act = () => TwoTrack.Ok()
                .SetExceptionFilter(ex => ex is ArgumentNullException || ex is ArgumentException)
                .Do(() => throw new ApplicationException());

            // Assert
            act.Should().Throw<ApplicationException>();

        }
    }
}
