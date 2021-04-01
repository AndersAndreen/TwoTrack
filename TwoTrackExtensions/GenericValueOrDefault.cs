using System;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericValueOrDefault
    {
        public static T ValueOrDefault<T>(this ITwoTrack<T> result, T defaultValue)
        {
            if (result is null) throw new ArgumentNullException(nameof(result));
            var outputValue = defaultValue;
            result.Select(value => outputValue = value);
            return outputValue;
        }
    }
}
