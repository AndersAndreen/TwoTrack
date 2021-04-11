using System;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericEncloseExtensions
    {
        public static ITwoTrack<(T1, T2)> Enclose<T1, T2>(this ITwoTrack<T1> result1, Func<T1, T2> @delegate)
        {
            return result1.Select(value1 => (value1, @delegate(value1)));
        }

        public static ITwoTrack<(T1, T2)> Enclose<T1, T2>(this ITwoTrack<T1> result1, Func<T1, ITwoTrack<T2>> @delegate)
        {
            return result1.Select(value1 => @delegate(value1).Select(value2 => (value1, value2)));
        }

        public static ITwoTrack<(T1, T2, T3)> Enclose<T1, T2, T3>(this ITwoTrack<(T1, T2)> result1, Func<(T1, T2), T3> @delegate)
        {
            return result1.Select(value1 => (value1.Item1, value1.Item2, @delegate(value1)));
        }

        public static ITwoTrack<(T1, T2, T3)> Enclose<T1, T2, T3>(this ITwoTrack<(T1, T2)> result1, Func<(T1,T2), ITwoTrack<T3>> @delegate)
        {
            return result1.Select(value1 => @delegate(value1).Select(value2 => (value1.Item1, value1.Item2, value2)));
        }
    }
}