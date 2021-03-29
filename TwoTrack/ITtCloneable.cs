using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult
{
    public interface ITtCloneable : ITwoTrackResult
    {
        Func<Exception, bool> ExceptionFilter { get; }
    }

}
