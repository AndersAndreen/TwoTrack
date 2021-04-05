using System;
using System.Collections.Generic;
using System.Text;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class NonGenericReplaceErrorMessageExtensions
    {
        public static ITwoTrack ReplaceNullResultsWithErrorMessage(this ITwoTrack source, string category, string decription)
        {
            return source;

        }
    }
}
