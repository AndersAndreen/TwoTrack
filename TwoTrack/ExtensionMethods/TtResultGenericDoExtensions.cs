using System;

namespace TwoTrackResult
{
    public static class TtResultGenericDoExtensions
    {
        public static ITwoTrack<T> Do<T>(this ITwoTrack<T> resultIn, Action func)
        {
            if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
            return resultIn.TryCatch(() =>
            {
                func();
                return resultIn;
            });
        }

        public static ITwoTrack<T> TryCatch<T>(this ITwoTrack<T> resultIn, Func<ITwoTrack<T>> func)
        {
            if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
            if (resultIn.Failed) return resultIn;
            try
            {
                return func();
            }
            catch (Exception e) when (resultIn.ExceptionFilter(e))
            {
                return resultIn.AddError(TtError.Exception(e));
            }
        }
    }
}
