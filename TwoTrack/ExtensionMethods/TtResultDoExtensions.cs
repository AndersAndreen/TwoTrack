using System;

namespace TwoTrackResult
{
    public static class DoExtensions
    {
        public static ITwoTrack Do(this ITwoTrack resultIn, Action action)
        {
            if (action is null) return resultIn.AddError(TtError.ArgumentNullError());
            return resultIn.TryCatch(() =>
            {
                action();
                return resultIn;
            });
        }

        public static ITwoTrack Do<T>(this ITwoTrack resultIn, Func<ITwoTrack<T>> func)
        {
            if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
            return resultIn.TryCatch(() =>
            {
                func();
                return resultIn;
            });
        }

        private static ITwoTrack TryCatch(this ITwoTrack resultIn, Func<ITwoTrack> func)
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
