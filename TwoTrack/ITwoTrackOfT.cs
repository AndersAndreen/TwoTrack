using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public interface ITwoTrack<out T>: ITwoTrackResult
    {
        Func<Exception, bool> ExceptionFilter { get; }

        ITwoTrack<T> AddError(TtError error);
        ITwoTrack<T> AddErrors(IEnumerable<TtError> errors);
        ITwoTrack<T> AddErrors(TtResult result);
        ITwoTrack<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter);
        ITwoTrack<T> Do<T2>(Func<T, T2> func);
        ITwoTrack<T2> Select<T2>(Func<T, T2> func);
        ITwoTrack<T> Do(Action onFailure, Action<T> onSuccess);
    }
}
