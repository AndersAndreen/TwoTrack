using System;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericEncloseExtensions
    {
        public static ITwoTrack<T> Enclose<T>(this ITwoTrack source, Func<T> func)
            => source.Failed
                ? TwoTrackCore.TwoTrack.Fail<T>(source.Errors)
                : TwoTrackCore.TwoTrack.Enclose(func);

        public static ITwoTrack<T> Enclose<T>(this ITwoTrack source, Func<ITwoTrack<T>> func)
            => source.Failed
                ? TwoTrackCore.TwoTrack.Fail<T>(source.Errors)
                : TwoTrackCore.TwoTrack.Enclose(func);

        public static ITwoTrack<(T1, T2)> Enclose<T1, T2>(this ITwoTrack<T1> result1, Func<T1, T2> @delegate)
        {
            return result1.Select(value1 => (value1, @delegate(value1)));
        }

        public static ITwoTrack<(T, T2)> Enclose<T, T2>(this ITwoTrack<T> result1, Func<T, ITwoTrack<T2>> @delegate)
        {
            return result1.Select(value1 => @delegate(value1).Select(value2 => (value1, value2)));
        }
    }
}