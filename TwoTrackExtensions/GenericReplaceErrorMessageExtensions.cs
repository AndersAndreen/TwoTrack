using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericReplaceErrorMessageExtensions
    {
        public static ITwoTrack<T> ReplaceNullResultsWithReportError<T>(this ITwoTrack<T> source, string category, string description)
            => source.ReplaceErrorsByCategory(category, TwoTrackError.ReportError(category, description));
    }
}
