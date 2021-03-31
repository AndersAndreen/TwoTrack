using System;

namespace TwoTrackCore
{
    public interface ITtCloneable : ITwoTrackResult
    {
        Func<Exception, bool> ExceptionFilter { get; }
    }

}
