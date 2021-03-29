using System;

namespace TwoTrack.Core
{
    public interface ITtCloneable : ITwoTrackResult
    {
        Func<Exception, bool> ExceptionFilter { get; }
    }

}
