using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoTrackResult
{
    public class TtResult : TtResultBase<TtResult>
    {
        private TtResult()
        {
        }

        public TtResult AddError(TtError error) => AddError(new TtResult(), error);
        public TtResult AddErrors(IEnumerable<TtError> errors) => AddErrors(new TtResult(), errors);
        public TtResult AddErrors(TtResult result) => AddErrors(new TtResult(), result?.Errors);

        public TtResult SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return AddError(new TtResult(), null);
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public TtResult Do(Action action)
        {
            if (action is null) return AddError(new TtResult(), null);
            if (Failed) return this;
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

        #region Factory methods
        internal static TtResult Ok() => new TtResult();

        internal static TtResult Fail(TtError defaultError)
        {
            return defaultError is null
                ? new TtResult().AddError(TtError.ArgumentNullError())
                : new TtResult().AddError(defaultError);
        }

        internal static TtResult Fail(IEnumerable<TtError> defaultError)
        {
            var err = defaultError?.ToList();
            return defaultError is null || !err.Any()
                ? new TtResult().AddError(TtError.ArgumentNullError())
                : new TtResult().AddErrors(err);
        }

        #endregion
    }
}
