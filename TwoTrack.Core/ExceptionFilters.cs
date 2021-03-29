using System;

namespace TwoTrack.Core
{
    public class ExceptionFilters
    {
        public static Func<Exception, bool> CatchNone = ex => false;
        public static Func<Exception, bool> CatchAll = ex => true;
    }
}
