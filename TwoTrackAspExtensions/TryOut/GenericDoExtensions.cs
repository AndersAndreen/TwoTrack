using System;

namespace TwoTrackResult
{
    public static class TwoTrackGenericDoExtensions
    {
        public static ITwoTrack<T> Do<T>(this ITwoTrack<T> resultIn, Action action)
        {
            if (action is null) return resultIn.AddError(TtError.ArgumentNullError());
            if (resultIn.Failed) return resultIn;
            return resultIn.Select(() =>
            {
                action();
                return resultIn;
            });
        }
    }
}
