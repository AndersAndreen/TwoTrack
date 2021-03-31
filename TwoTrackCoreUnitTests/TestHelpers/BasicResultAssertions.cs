using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using TwoTrackCore;

namespace TwoTrackCoreUnitTests.TestHelpers
{
    public static class BasicResultAssertions
    {
        public static void AssertBasicSuccessCriteria(this ITwoTrack result)
        {
            using (new AssertionScope())
            {
                result.Failed.Should().BeFalse();
                result.Succeeded.Should().BeTrue();
                // Warnings do not count as failure criteria, so we filter them out
                result.Errors.Where(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning)
                    .Should().BeEmpty();
            }
        }

        public static void AssertBasicAppResultFailureCriteria(this ITwoTrack result)
        {
            using (new AssertionScope())
            {
                result.Failed.Should().BeTrue();
                result.Succeeded.Should().BeFalse();
                // Warnings do not count as failure criteria, so we filter them out
                result.Errors.Where(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning)
                    .Should().NotBeEmpty();
            }
        }
        public static void AssertBasicSuccessCriteria(this ITwoTrack<int> result)
        {
            using (new AssertionScope())
            {
                result.Failed.Should().BeFalse();
                result.Succeeded.Should().BeTrue();
                // Warnings do not count as failure criteria, so we filter them out
                result.Errors.Where(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning)
                    .Should().BeEmpty();
            }
        }

        public static void AssertBasicAppResultFailureCriteria(this ITwoTrack<int> result)
        {
            using (new AssertionScope())
            {
                result.Failed.Should().BeTrue();
                result.Succeeded.Should().BeFalse();
                // Warnings do not count as failure criteria, so we filter them out
                result.Errors.Where(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning)
                    .Should().NotBeEmpty();
            }
        }
    }
}
