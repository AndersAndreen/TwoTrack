using System;

namespace TwoTrackResult
{
    public static class TryCatchExtensions
    {
        //public static ITwoTrack TryCatch(this ITwoTrack resultIn, Func<ITwoTrack> func)
        //{
        //    if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
        //    if (resultIn.Failed) return resultIn;
        //    try
        //    {
        //        return func();
        //    }
        //    catch (Exception e) when (resultIn.ExceptionFilter(e))
        //    {
        //        return resultIn.AddError(TtError.Exception(e));
        //    }
        //}

        //public static ITwoTrack<T> TryCatch<T>(this ITwoTrack<T> resultIn, Func<ITwoTrack<T>> func)
        //{
        //    if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
        //    if (resultIn.Failed) return resultIn;
        //    try
        //    {
        //        return func();
        //    }
        //    catch (Exception e) when (resultIn.ExceptionFilter(e))
        //    {
        //        return resultIn.AddError(TtError.Exception(e));
        //    }
        //}
    }
}
