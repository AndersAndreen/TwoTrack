using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult
{
    public static class TtResultSelectExtensions
    {

        public static ITwoTrack<T2> Select<T2>(this ITwoTrack resultIn, Func<T2> func)
        {
            if (func is null) return TtResult<T2>.Fail(TtError.ArgumentNullError());
            if (resultIn.Failed) return TtResult<T2>.Fail(resultIn.Errors);
            return  TwoTrack.Enclose(func);
        }
    }
}
