﻿using System;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class NonGenericSelectExtensions
    {
        public static ITwoTrack<T2> Select<T2>(this ITwoTrack resultIn, Func<ITwoTrack<T2>> func)
        {
            if (func is null) return TwoTrack.Fail<T2>(TtError.ArgumentNullError());
            return resultIn.Failed
                ? TwoTrack.Fail<T2>(resultIn.Errors)
                : func();
        }

        public static ITwoTrack<T2> Select<T, T2>(this ITwoTrack<T> resultIn, Func<T2> func)
        {
            if (func is null) return TwoTrack.Fail<T2>(TtError.ArgumentNullError());
            return resultIn.Failed
                ? TwoTrack.Fail<T2>(resultIn.Errors)
                : TwoTrack.Enclose(func);
        }

        public static ITwoTrack<T2> Select<T, T2>(this ITwoTrack<T> resultIn, Func<ITwoTrack<T2>> func)
        {
            if (func is null) return TwoTrack.Fail<T2>(TtError.ArgumentNullError());
            return resultIn.Failed
                ? TwoTrack.Fail<T2>(resultIn.Errors)
                : func();
        }
    }
}
