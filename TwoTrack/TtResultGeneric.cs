using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public class TtResult<T> : TtResultBase<TtResult<T>>
    {
        private TtResult()
        {
        }

        public TtResult<T> AddError(TtError error) => AddError(new TtResult<T>(), error);
        public TtResult<T> AddErrors(IEnumerable<TtError> errors) => AddErrors(new TtResult<T>(), errors);
        public TtResult<T> AddErrors(TtResult result) => AddErrors(new TtResult<T>(), result?.Errors);

        public TtResult<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return AddError(new TtResult<T>(), null);
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public TtResult<T> Do(Action action)
        {
            if (action is null) return AddError(new TtResult<T>(), null);
            try
            {
                action();
                return this;
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return new TtResult<T>().AddError(TtError.Exception(e));
            }
        }

        #region Factory methods
        internal static TtResult<T> Fail(TtError defaultError)
        {
            return defaultError is null
                ? new TtResult<T>().AddError(TtError.ArgumentNullError())
                : new TtResult<T>().AddError(defaultError);
        }

        internal static TtResult<T> Fail(IEnumerable<TtError> defaultError)
        {
            var err = defaultError?.ToList();
            return defaultError is null || !err.Any()
                ? new TtResult<T>().AddError(TtError.ArgumentNullError())
                : new TtResult<T>().AddErrors(err);
        }

        #endregion

    }
}
