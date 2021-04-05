using System;
using System.Collections.Generic;
using System.Text;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericReplaceErrorMessageExtensions
    {
        public static ITwoTrack<T> ReplaceNullResultsWithErrorMessage<T>(this ITwoTrack<T> source, string category, string decription)
        {
            var error = TwoTrackError.Error(ErrorLevel.ReportError, category, decription);
            return source;
        }
    }
}
