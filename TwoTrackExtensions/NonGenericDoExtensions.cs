namespace TwoTrackExtensions
{
    public static class NonGenericDoExtensions
    {
        //public static ITwoTrack Do(this ITwoTrack resultIn, Action action)
        //{
        //    if (action is null) return resultIn.AddError(TtError.ArgumentNullError());
        //    if (resultIn.Failed) return resultIn;
        //    return resultIn.Do(TryCatch(() =>
        //    {
        //        action();
        //        return resultIn;
        //    });
        //}

        //public static ITwoTrack Do(this ITwoTrack resultIn, Func<ITwoTrack> func)
        //{
        //    if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
        //    if (resultIn.Failed) return resultIn;
        //    return resultIn.TryCatch(func);
        //}

        //public static ITwoTrack Do<T>(this ITwoTrack resultIn, Func<ITwoTrack<T>> func)
        //{
        //    if (func is null) return resultIn.AddError(TtError.ArgumentNullError());
        //    if (resultIn.Failed) return resultIn;
        //    return resultIn.TryCatch(() =>
        //    {
        //        var result = func();
        //        return resultIn.AddErrors(result.Errors);
        //    });
        //}


    }
}
