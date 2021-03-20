using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public class TtResult : TtResultBase<TtResult>
    {
        internal TtResult()
        {
        }

        public TtResult AddError(TtError error) => AddError(new TtResult(), error);
        public TtResult AddErrors(IEnumerable<TtError> errors) => AddErrors(new TtResult(), errors);
        public TtResult AddErrors(TtResult result) => AddErrors(new TtResult(), result?.Errors);

        public TtResult SetTryCatchFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return AddError(new TtResult(),null);
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public TtResult Do(Action action)
        {
            if (action is null) return AddError(new TtResult(), null);
            try
            {
                action();
                return this;
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return new TtResult().AddError(TtError.Exception(e));
            }

        }


    }
}
