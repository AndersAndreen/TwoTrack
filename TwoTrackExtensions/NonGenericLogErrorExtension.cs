using System;
using System.Collections.Generic;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class NonGenericLogErrorExtension
    {
        public static ITwoTrack LogErrors(this ITwoTrack source, Action<IEnumerable<TtError>> logger)
        {
            if (source.Failed) logger(source.Errors);
            return source;
        }

    }
}
