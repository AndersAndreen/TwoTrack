using System;
using System.Collections.Generic;

namespace TwoTrackCore
{
    public interface ITwoTrack : ITtCloneable
    {
        ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter);

        ITwoTrack AddError(TwoTrackError error);
        ITwoTrack AddErrors(IEnumerable<TwoTrackError> errors);
        ITwoTrack ReplaceErrorsByCategory(string Category, TwoTrackError replacement);

        /// <summary> If succeeded: Add a confirmation.</summary>
        ITwoTrack AddConfirmation(TtConfirmation confirmation);
       
        /// <summary> If succeeded: Add some confirmations.</summary>
        ITwoTrack AddConfirmations(IEnumerable<TtConfirmation> confirmations);

        /// <summary> If succeeded: Execute action.</summary>
        ITwoTrack Do(Action action);

        /// <summary> If succeeded: Execute func and merge result.</summary>
        ITwoTrack Do(Func<ITwoTrack> func);

        /// <summary> If succeeded: Execute func and merge result.</summary>
        ITwoTrack Do<T>(Func<ITwoTrack<T>> func);

        /// <summary> If succeeded: Execute func, merge result and change type to <see cref="ITwoTrack{T}"/>.</summary>
        ITwoTrack<T> Enclose<T>(Func<T> func);
    }
}
