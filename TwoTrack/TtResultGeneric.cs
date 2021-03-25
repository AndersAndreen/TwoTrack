using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public class TtResult<T> : TtResultBase<TtResult<T>>, ITwoTrack<T>
    {
        private T _value;

        private TtResult()
        {
        }

        public ITwoTrack<T> AddError(TtError error) => AddError(new TtResult<T>(), error);
        public ITwoTrack<T> AddErrors(IEnumerable<TtError> errors) => AddErrors(new TtResult<T>(), errors);
        public ITwoTrack<T> AddErrors(TtResult result) => AddErrors(new TtResult<T>(), result?.Errors);

        public ITwoTrack<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return AddError(new TtResult<T>(), TtError.ArgumentNullError());
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public ITwoTrack<T> Do<T2>(Func<T, T2> func)
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


        public ITwoTrack<T2> Select<T2>(Func<T, T2> func)
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

        public ITwoTrack<T2> Select<T2>(Func<T, ITwoTrack<T2>> func)
        {
            if (func is null) return TtResult<T2>.Fail(TtError.ArgumentNullError());
            if (Failed) return TtResult<T2>.Fail(Errors);
            var result = new TtResult<T2> { };
            if (!Succeeded) return result.AddErrors(Errors);
            try
            {
                return func(_value);
            }
            catch (Exception e) when (result.ExceptionFilter(e))
            {
                return TtResult<T2>.Fail(TtError.Exception(e));
            }
        }

        public ITwoTrack<T> Do(Action onFailure, Action<T> onSuccess)
        {
            if (Failed) onFailure();
            if (Succeeded) onSuccess(_value);
            return this;
        }

        #region Factory methods
        internal static ITwoTrack<T> Fail(TtError defaultError)
        {
            return defaultError is null
                ? new TtResult<T>().AddError(TtError.ArgumentNullError())
                : new TtResult<T>().AddError(defaultError);
        }

        internal static ITwoTrack<T> Fail(IEnumerable<TtError> errors)
        {
            var errorList = errors?.ToList();
            return errors is null || !errorList.Any()
                ? new TtResult<T>().AddError(TtError.ArgumentNullError())
                : new TtResult<T>().AddErrors(errorList);
        }

        internal static ITwoTrack<T> Enclose(Func<T> func)
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

        internal static ITwoTrack<T> Enclose(Func<ITwoTrack<T>> func)
        {
            if (func is null) return new TtResult<T>().AddError(TtError.ArgumentNullError());
            var result = new TtResult<T>();
            try
            {
                var result2 = func();
                return result2;
            }
            catch (Exception e) when (result.ExceptionFilter(e))
            {
                return new TtResult<T>().AddError(TtError.Exception(e));
            }
        }
        #endregion

    }
}
