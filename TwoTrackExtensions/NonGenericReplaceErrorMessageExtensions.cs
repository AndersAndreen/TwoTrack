using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class NonGenericReplaceErrorMessageExtensions
    {
        public static ITwoTrack ReplaceNullResultsWithReportError(this ITwoTrack source, string category, string description)
            => source.ReplaceErrorsByCategory(category, TwoTrackError.ReportError(category, description));

    }
}
