using System;
using System.Collections.Generic;
using System.Text;
using TwoTrackResult;

namespace TwoTrackAspExtensions.TryOut
{
    public static class NonGenericAddErrorsAndConfirmationsExtensions
    {
        public static ITwoTrack AddErrors<T>(this ITwoTrack source, ITwoTrack result) => source.AddErrors(result.Errors);
        public static ITwoTrack AddErrors<T2, T>(this ITwoTrack source, ITwoTrack<T2> result) => source.AddErrors(result.Errors);

        public static ITwoTrack AddConfirmations<T>(this ITwoTrack source, ITwoTrack result) => source.AddConfirmations(result.Confirmations);
        public static ITwoTrack AddConfirmations<T2, T>(this ITwoTrack source, ITwoTrack<T2> result) => source.AddConfirmations(result.Confirmations);
    }
}
