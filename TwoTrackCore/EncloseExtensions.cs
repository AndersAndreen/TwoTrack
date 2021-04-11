using System;

namespace TwoTrackCore
{
    public static class EncloseExtensions
    {

        public static ITwoTrack<T> Enclose<T>(this ITwoTrack source, Func<ITwoTrack<T>> func)
            => source.Failed
                ? TwoTrack.Fail<T>(source)
                : TwoTrack.Enclose(func);
    }
}
