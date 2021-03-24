using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public interface ITwoTrack<out T>
    {
        ITwoTrack<T> AddError(TtError error);
        ITwoTrack<T> AddErrors(IEnumerable<TtError> errors);
        ITwoTrack<T> AddErrors(TtResult result);
        ITwoTrack<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter);
        ITwoTrack<T> Do(Action func);
        ITwoTrack<T> Do<T2>(Func<T, T2> func);
        ITwoTrack<T2> Select<T2>(Func<T, T2> func);
        bool Failed { get; }
        bool Succeeded { get; }
        IReadOnlyCollection<TtError> Errors { get; }
        IReadOnlyCollection<TtConfirmation> Confirmations { get; }
        Func<Exception, bool> ExceptionFilter { get; }
    }
}
