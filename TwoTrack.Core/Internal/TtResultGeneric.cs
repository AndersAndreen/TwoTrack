using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TwoTrack.Core.Internal
{
    internal class TtResult<T> : TtResultBase<TtResult<T>>, ITwoTrack<T>, ITtCloneable
    {
        private T _value;

        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning)

        ;
        public bool Succeeded => !Failed;

        private TtResult()
        {
        }

        public ITwoTrack<T> AddError(TtError error) => CloneAndSet(_value).AppendError(error);
        public ITwoTrack<T> AddErrors(IEnumerable<TtError> errors) => CloneAndSet(_value).AppendErrors(errors);

        public ITwoTrack<T> AddConfirmation(TtConfirmation confirmation) => CloneAndSet(_value).AppendConfirmation(confirmation);
        public ITwoTrack<T> AddConfirmations(IEnumerable<TtConfirmation> confirmations) => CloneAndSet(_value).AppendConfirmations(confirmations);


        public ITwoTrack<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return CloneAndSet(_value, TtError.ArgumentNullError());
            CloneAndSet(_value).ExceptionFilter = exeptionFilter;
            return this;
        }

        public ITwoTrack<T> Do(Action onFailure, Action<T> onSuccess)
        {
            if (onFailure is null || onSuccess is null) return CloneAndSet(_value, TtError.ArgumentNullError());
            if (Failed) onFailure();
            if (Succeeded) return AddErrors(TryCatch(() =>
            {
                onSuccess(_value);
                return this;
            }).Errors);
            return this;
        }

        public ITwoTrack<T> Do<T2>(Func<T2> func)
        {
            if (func is null) return CloneAndSet(_value, TtError.ArgumentNullError());
            return Succeeded ? AddErrors(TryCatch(func).Errors) : this;
        }

        public ITwoTrack<T> Do<T2>(Func<T, T2> func) => Do(() => func(_value));



        public ITwoTrack<T2> Select<T2>(Func<T2> func)
        {
            if (func is null) return CloneAndSet(default(T2), TtError.ArgumentNullError());
            return Failed
                ? CloneAndSet(default(T2), TtError.ArgumentNullError())
                : TryCatch(func);
        }

        public ITwoTrack<T2> Select<T2>(Func<T, T2> func) => Select(() => func(_value));

        public ITwoTrack<T2> Select<T2>(Func<ITwoTrack<T2>> func)
        {
            if (func is null) return CloneAndSet(default(T2), TtError.ArgumentNullError());
            return Failed
                ? CloneAndSet(default(T2), TtError.ArgumentNullError())
                : TryCatch(() =>
                {
                    T2 value = default;
                    func().Do(val => value = val);
                    return value;
                });

        }

        public ITwoTrack<T2> Select<T2>(Func<T, ITwoTrack<T2>> func) => Select(() => func(_value));


        protected ITwoTrack<T2> TryCatch<T2>(Func<T2> func)
        {
            try
            {
                return CloneAndSet(func(), TtError.ResultNullError());
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return CloneAndSet(default(T2), TtError.Exception(e));
            }
        }


        protected TtResult<T> ErrorIfNullOrTupleContainsNull(T input, TtError error)
        {
            var tupleHasNull = false;
            if (input is ITuple tuple)
            {
                for (var i = 0; i < tuple.Length; i++)
                {
                    tupleHasNull = tupleHasNull || tuple[i] is null;
                }
            }
            if (input is null || tupleHasNull)
            {
                AppendError(error);
            }
            return this;
        }


        private TtResult<T2> CloneAndSet<T2>(T2 value, TtError error = default)
        {
            var clone = new TtResult<T2>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            clone._value = value;
            return clone.Succeeded
                ? clone.ErrorIfNullOrTupleContainsNull(value, error ?? TtError.DesignBugError())
                : clone;
        }

        #region Factory methods
        public static ITwoTrack<T> Enclose(Func<T> func) => new TtResult<T>().Select(func);
        public static ITwoTrack<T> Enclose(Func<ITwoTrack<T>> func) => new TtResult<T>().Select(func);
        public static ITwoTrack<T> Enclose(Func<T> func, Func<T, bool> validator) => new TtResult<T>().Select(func); // TODO: implement validator!
        public static ITwoTrack<T> Enclose(ITwoTrack source, Func<T> func) => new TtResult<T>().Select(func);

        internal static ITwoTrack<T> Fail(TtError error)
        {
            return error is null
                ? new TtResult<T>().AppendError(TtError.ArgumentNullError())
                : new TtResult<T>().AppendError(error);
        }

        internal static ITwoTrack<T> Fail(IEnumerable<TtError> errors)
        {
            var errorList = errors?.ToList();
            return errors is null || !errorList.Any()
                ? new TtResult<T>().AppendError(TtError.ArgumentNullError())
                : new TtResult<T>().AppendErrors(errorList);
        }

        internal static ITwoTrack<T> Fail(ITtCloneable source, TtError error = default)
        {
            var clone = new TtResult<T>
            {
                ExceptionFilter = source.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(source.Errors);
            clone.ConfirmationsList.AddRange(source.Confirmations);
            if (source.Succeeded) return clone.AppendError(error ?? TtError.DefaultError());
            return error is null
                ? clone
                : clone.AddError(error);
        }
        #endregion
    }
}
