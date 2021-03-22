using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public class TtResult<T> : TtResultBase<TtResult<T>>
    {
        private T _value;

        private TtResult()
        {
        }

        public TtResult<T> AddError(TtError error) => AddError(new TtResult<T>(), error);
        public TtResult<T> AddErrors(IEnumerable<TtError> errors) => AddErrors(new TtResult<T>(), errors);
        public TtResult<T> AddErrors(TtResult result) => AddErrors(new TtResult<T>(), result?.Errors);

        public TtResult<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return AddError(new TtResult<T>(), TtError.ArgumentNullError());
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public TtResult<T> Do<T2>(Func<T, T2> func)
        {
            if (func is null) return AddError(new TtResult<T>(), TtError.ArgumentNullError());
            if (Failed) return this;
            try
            {
                _ = func(_value);
                return this;
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return AddError(new TtResult<T>(), TtError.Exception(e));
            }
        }
        public TtResult<T2> Select<T2>(Func<T, T2> func)
        {
            if (func is null) return TtResult<T2>.Fail(TtError.ArgumentNullError());
            if (Failed) return TtResult<T2>.Fail(Errors);
            var result = new TtResult<T2> { };
            if (!Succeeded) return result.AddErrors(Errors);
            try
            {
                result._value = func(_value);
                if (result._value == null) result.AddError(TtError.ResultNullError());
                return result;
            }
            catch (Exception e) when (result.ExceptionFilter(e))
            {
                return TtResult<T2>.Fail(TtError.Exception(e));
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

        public static TtResult<T> Enclose(Func<T> func)
        {
            if (func is null) return new TtResult<T>().AddError(TtError.ArgumentNullError());
            var result = new TtResult<T>();
            try
            {
                result._value = func();
                if (result._value == null) result.AddError(TtError.ResultNullError());
                return result;
            }
            catch (Exception e) when (result.ExceptionFilter(e))
            {
                return new TtResult<T>().AddError(TtError.Exception(e));
            }
        }

        public static TtResult<T> Enclose(Func<TtResult<T>> func) => Enclose(() => func()._value);

        #endregion

    }
}
