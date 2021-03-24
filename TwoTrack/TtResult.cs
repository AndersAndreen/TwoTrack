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

        public ITwoTrack Do(Action func)
        {
            if (func is null) return AddError(new TtResult(), TtError.ArgumentNullError());
            if (Failed) return this;
            try
            {
                func();
                return this;
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return AddError(new TtResult(), TtError.Exception(e));
            }
        }

        public ITwoTrack Do<T>(Func<T> func)
        {
            if (func is null) return AddError(new TtResult(), TtError.ArgumentNullError());
            if (Failed) return this;
            try
            {
                _ = func();
                return this;
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return AddError(new TtResult(), TtError.Exception(e));
            }
        }

        public ITwoTrack Do<T>(Func<ITwoTrack<T>> func)
        {
            if (func is null) return AddError(new TtResult(), TtError.ArgumentNullError());
            if (Failed) return this;
            try
            {
                var result = func();
                return this.AddErrors(result.Errors);
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return AddError(new TtResult(), TtError.Exception(e));
            }
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
