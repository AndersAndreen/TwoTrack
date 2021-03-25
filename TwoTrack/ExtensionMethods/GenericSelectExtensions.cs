using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult.ExtensionMethods
{
    static class GenericSelectExtensions
    {
        public static ITwoTrack<T2> Select<T, T2>(this ITwoTrack<T> resultIn, Func<T2> func)
        {
            if (func is null) return TtResult<T2>.Fail(TtError.ArgumentNullError());
            if (resultIn.Failed) return TtResult<T2>.Fail(resultIn.Errors);
            return resultIn.Select(inp => func());
        }
    }
}
