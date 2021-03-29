using System;
using System.Collections.Generic;

namespace TwoTrack.Core
{
    public interface ITwoTrack : ITtCloneable
    {
        ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter);

        ITwoTrack AddError(TtError error);
        ITwoTrack AddErrors(IEnumerable<TtError> errors);
        ITwoTrack AddConfirmation(TtConfirmation confirmation);
        ITwoTrack AddConfirmations(IEnumerable<TtConfirmation> confirmations);

        ITwoTrack Do(Action action);
        ITwoTrack Do(Func<ITwoTrack> func);
        ITwoTrack Do<T>(Func<ITwoTrack<T>> func);
    }
}
