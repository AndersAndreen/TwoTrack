using System;
using System.Collections.Generic;

namespace TwoTrackCore
{
    public interface ITwoTrack<out T> : ITtCloneable
    {
        ITwoTrack<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter);

        ITwoTrack<T> AddError(TtError error);
        ITwoTrack<T> AddErrors(IEnumerable<TtError> errors);
        ITwoTrack<T> AddConfirmation(TtConfirmation confirmation);
        ITwoTrack<T> AddConfirmations(IEnumerable<TtConfirmation> confirmations);

        ITwoTrack<T> Do(Action<T> func);
        ITwoTrack<T> Do<T2>(Func<T, T2> func);
        ITwoTrack<T> Do(Action onFailure, Action<T> onSuccess);

        ITwoTrack<T2> Select<T2>(Func<T2> func);
        ITwoTrack<T2> Select<T2>(Func<T, T2> func);
        ITwoTrack<T2> Select<T2>(Func<ITwoTrack<T2>> func);
        ITwoTrack<T2> Select<T2>(Func<T, ITwoTrack<T2>> func);
    }
}

