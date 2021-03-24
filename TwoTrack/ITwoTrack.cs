using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public interface ITwoTrack
    {
        bool Failed { get; }
        bool Succeeded { get; }
        IReadOnlyCollection<TtError> Errors { get; }
        IReadOnlyCollection<TtConfirmation> Confirmations { get; }
        Func<Exception, bool> ExceptionFilter { get; }

        ITwoTrack AddError(TtError error);
        ITwoTrack AddErrors(IEnumerable<TtError> errors);
        ITwoTrack AddErrors(ITwoTrack result);
        ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter);
    }
}
