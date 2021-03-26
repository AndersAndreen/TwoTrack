using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public interface ITwoTrack : ITwoTrackResult
    {
        Func<Exception, bool> ExceptionFilter { get; }

        ITwoTrack AddError(TtError error);
        ITwoTrack AddErrors(IEnumerable<TtError> errors);
        ITwoTrack AddErrors(ITwoTrack result);
        ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter);
    }
}
