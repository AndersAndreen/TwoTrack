using System;
using System.Collections.Generic;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericLogErrorExtension
    {
        public static ITwoTrack<T> LogErrors<T>(this ITwoTrack<T> source, Action<IEnumerable<TtError>> logger)
        {
            if (source.Failed) logger(source.Errors);
            return source;
        }
    }
}
