using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TwoTrackCore.Defaults;

namespace TwoTrackCore.Internal
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

        #region Public instance methods
        public ITwoTrack<T> AddError(TwoTrackError error) => CloneAndSet(_value).AppendError(error);
        public ITwoTrack<T> AddErrors(IEnumerable<TwoTrackError> errors) => CloneAndSet(_value).AppendErrors(errors);
        public ITwoTrack<T> ReplaceErrorsByCategory(string Category, TwoTrackError replacement)
        {
            if (!Errors.Any(error => error.Category == Category)) return this;

            var clone = new TtResult<T>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors.Where(error => error.Category != ErrorCategory.ResultNullError).Concat(new[] { replacement }));
            clone.ConfirmationsList.AddRange(Confirmations);
            clone._value = _value;
            return clone;
        }

        public ITwoTrack<T> AddConfirmation(TtConfirmation confirmation) => CloneAndSet(_value).AppendConfirmation(confirmation);
        public ITwoTrack<T> AddConfirmations(IEnumerable<TtConfirmation> confirmations) => CloneAndSet(_value).AppendConfirmations(confirmations);

        public ITwoTrack<T> SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return CloneAndSet(_value, TwoTrackError.ArgumentNullError());
            var clone = Clone();
            clone.ExceptionFilter = exeptionFilter;
            return clone;
        }

        public ITwoTrack<T> Do(Action onFailure, Action<T> onSuccess)
        {
            if (onFailure is null || onSuccess is null) return CloneAndSet(_value, TwoTrackError.ArgumentNullError());
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
            if (Failed) return this;
            if (func is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
            return MergeResultWith(TryCatch(() =>
            {
                func();
                return new TtResult<T>();
            }));
        }

        public ITwoTrack<T> Do<T2>(Func<T, T2> func)
        {
            if (Failed) return this;
            if (func is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
            return Do(() => func(_value));
        }

        public ITwoTrack<T> Do(Action<T> action)
        {
            if (Failed) return this;
            if (action is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
            var other = TryCatch(() =>
                {
                    action(_value);
                    return new TtResult<T>();
                });
            return MergeResultWith(other);
        }

        public ITwoTrack<T> Do(Func<ITwoTrack> func)
        {
            if (Failed) return this;
            if (func is null) return AddError(TwoTrackError.ArgumentNullError());
            return MergeResultWith(TryCatch(func));
        }

        public ITwoTrack<T> Do<T2>(Func<ITwoTrack<T2>> func)
        {
            if (Failed) return this;
            if (func is null) return AddError(TwoTrackError.ArgumentNullError());
            return TryCatch(() =>
            {
                var result = func();
                return AddErrors(result.Errors);
            });
        }

        public ITwoTrack<T2> Select<T2>(Func<T2> func)
        {
            if (Failed) return CloneAndChangeType<T2>();
            if (func is null) return CloneAndChangeType<T2>().AddError(TwoTrackError.ArgumentNullError());
            return TryCatch(()
                => new TtResult<T2>
                {
                    _value = func()
                }.MergeResultWith(this));
        }

        public ITwoTrack<T2> Select<T2>(Func<T, T2> func) => Select(() => func(_value));

        public ITwoTrack<T2> Select<T2>(Func<ITwoTrack<T2>> func)
        {
            if (Failed) return CloneAndChangeType<T2>();
            if (func is null) return CloneAndChangeType<T2>().AddError(TwoTrackError.ArgumentNullError());
            return CloneAndChangeType(TryCatch(func));
        }

        public ITwoTrack<T2> Select<T2>(Func<T, ITwoTrack<T2>> func) => Select(() => func(_value));

        public ITwoTrack<T> MergeResultWith(ITtCloneable other)
        {
            var clone = new TtResult<T>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ErrorsList.AddRange(other.Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            clone.ConfirmationsList.AddRange(other.Confirmations);
            clone._value = _value;
            return clone;
        }
        #endregion

        #region Private instance methods
        private ITwoTrack<T> TryCatch(Func<ITwoTrack> func)
        {
            try
            {
                var other = func();
                return new TtResult<T>().MergeResultWith(other);
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return new TtResult<T>().AppendError(TwoTrackError.Exception(e));
            }
        }

        private ITwoTrack<T2> TryCatch<T2>(Func<T2> func)
        {
            try
            {
                return new TtResult<T2>
                {
                    _value = func()
                };
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return new TtResult<T2>().AppendError(TwoTrackError.Exception(e));
            }
        }

        private ITwoTrack<T2> TryCatch<T2>(Func<ITwoTrack<T2>> func)
        {
            try
            {
                return func();
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return new TtResult<T2>().AppendError(TwoTrackError.Exception(e));
            }
        }

        //private TtResult<T2> TryCatch<T2>(Func<TtResult<T2>> func)
        //{
        //    try
        //    {
        //        return func();
        //    }
        //    catch (Exception e) when (ExceptionFilter(e))
        //    {
        //        return new TtResult<T2>().AppendError(TwoTrackError.Exception(e));
        //    }
        //}
        private TtResult<T> ErrorIfNullOrTupleContainsNull(T input, TwoTrackError error)
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
                ErrorsList.Add(error);
            }
            return this;
        }

        private TtResult<T> Clone()
        {
            var clone = new TtResult<T>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            clone._value = _value;
            return clone;
        }



        private TtResult<T2> CloneAndChangeType<T2>(ITwoTrack<T2> other)
        {
            var clone = new TtResult<T2>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            other.Do(otherValue => { clone._value = otherValue; });
            return clone;
        }

        private TtResult<T2> CloneAndChangeType<T2>()
        {
            var clone = new TtResult<T2>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            clone._value = default;
            return clone;
        }
        private TtResult<T2> CloneAndSet<T2>(T2 value, TwoTrackError error = default)
        {
            var clone = new TtResult<T2>
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            clone._value = value;
            return clone.Succeeded
                ? clone.ErrorIfNullOrTupleContainsNull(value, error ?? TwoTrackError.DesignBugError())
                : clone;
        }
        #endregion

        #region Factory methods
        public static ITwoTrack<T> Enclose(Func<T> func) => new TtResult<T>().Select(func);
        public static ITwoTrack<T> Enclose(Func<ITwoTrack<T>> func) => new TtResult<T>().Select(func);

        internal static ITwoTrack<T> Fail(TwoTrackError error)
        {
            return error is null
                ? new TtResult<T>().AppendError(TwoTrackError.ArgumentNullError())
                : new TtResult<T>().AppendError(error);
        }

        internal static ITwoTrack<T> Fail(IEnumerable<TwoTrackError> errors)
        {
            var errorList = errors?.ToList();
            return errors is null || !errorList.Any()
                ? new TtResult<T>().AppendError(TwoTrackError.ArgumentNullError())
                : new TtResult<T>().AppendErrors(errorList);
        }

        internal static ITwoTrack<T> Fail(ITtCloneable source, TwoTrackError error = default)
        {
            var clone = new TtResult<T>
            {
                ExceptionFilter = source.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(source.Errors);
            clone.ConfirmationsList.AddRange(source.Confirmations);
            if (source.Succeeded) return clone.AppendError(error ?? TwoTrackError.DefaultError());
            return error is null
                ? clone
                : clone.AddError(error);
        }
        #endregion
    }
}
