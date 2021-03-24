using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoTrackResult
{
    public class TtResult : TtResultBase<TtResult>, ITwoTrack
    {
        private TtResult()
        {
        }

        public ITwoTrack AddError(TtError error) => AddError(new TtResult(), error);
        public ITwoTrack AddErrors(IEnumerable<TtError> errors) => AddErrors(new TtResult(), errors);
        public ITwoTrack AddErrors(ITwoTrack result) => AddErrors(new TtResult(), result?.Errors);

        public ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return AddError(new TtResult(), null);
            ExceptionFilter = exeptionFilter;
            return this;
        }

        #region Factory methods
        internal static ITwoTrack Ok() => new TtResult();

        internal static ITwoTrack Fail(TtError defaultError)
        {
            return defaultError is null
                ? new TtResult().AddError(TtError.ArgumentNullError())
                : new TtResult().AddError(defaultError);
        }

        internal static ITwoTrack Fail(IEnumerable<TtError> defaultError)
        {
            var err = defaultError?.ToList();
            return defaultError is null || !err.Any()
                ? new TtResult().AddError(TtError.ArgumentNullError())
                : new TtResult().AddErrors(err);
        }

 
        #endregion
    }
}
