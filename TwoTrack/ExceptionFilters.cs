using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult
{
    public class ExceptionFilters
    {
        private Func<Exception, bool> CatchNone = ex => false;
        private Func<Exception, bool> CatchAll = ex => true;
    }
}
