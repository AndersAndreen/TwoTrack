using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackCore.Defaults;

namespace TwoTrackCore.Internal
{
    internal class TtResult : TtResultBase<TtResult>, ITwoTrack
    {
        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;

        private TtResult()
        {
        }

        #region Public instance methods
        public ITwoTrack AddError(TwoTrackError error)
        {
            if (error is null) return ArgumentNullError(4);
            return TryCatch(() => Clone().AppendError(error)); //Todo: if exception add a designbug error also
        }

        public ITwoTrack AddErrors(IEnumerable<TwoTrackError> errors)
        {
            if (errors is null) return ArgumentNullError(4);
            return TryCatch(() => Clone().AppendErrors(errors)); //Todo: if exception add a designbug error also
        }

        public ITwoTrack ReplaceErrorsByCategory(string Category, TwoTrackError replacement)
        {
            if (!Errors.Any(error => error.Category == Category)) return this;

            var clone = new TtResult
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors.Where(error => error.Category != ErrorCategory.ResultNullError).Concat(new[] { replacement }));
            clone.ConfirmationsList.AddRange(Confirmations);
            return clone;
        }

        public ITwoTrack AddConfirmation(TtConfirmation confirmation)
        {
            if (confirmation is null) return ArgumentNullError(4);
            return Failed
                ? this
                : TryCatch(() => Clone().AppendConfirmation(confirmation));
        }

        public ITwoTrack AddConfirmations(IEnumerable<TtConfirmation> confirmations)
        {
            if (confirmations is null) return ArgumentNullError(4);
            return Failed
                ? this
                : TryCatch(() => Clone().AppendConfirmations(confirmations));
        }

        public ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return ArgumentNullError(4);
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public ITwoTrack Do(Action action)
        {
            if (action is null) return ArgumentNullError(4);
            if (Failed) return this;
            return MergeResultWith(TryCatch(() =>
            {
                action();
                return new TtResult();
            }));
        }

        public ITwoTrack Do(Func<ITwoTrack> func)
        {
            if (func is null) return ArgumentNullError(4);
            if (Failed) return this;
            return MergeResultWith(TryCatch(func));
        }

        public ITwoTrack Do<T>(Func<ITwoTrack<T>> func)
        {
            if (func is null) return ArgumentNullError(4);
            if (Failed) return this;
            return TryCatch(() =>
            {
                var result = func();
                return AddErrors(result.Errors);
            });
        }

        public ITwoTrack<T> Enclose<T>(Func<T> func) => TtResult<T>.Enclose(this, func);
        #endregion

        #region Private instance methods
        private ITwoTrack ArgumentNullError(int skipFrames) => Clone().AppendError(TwoTrackError.ArgumentNullError(skipFrames));

        private ITwoTrack TryCatch(Func<ITwoTrack> func)
        {
            try
            {
                return func();
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return new TtResult().AppendError(TwoTrackError.Exception(e));
            }
        }

        private TtResult Clone()
        {
            var clone = new TtResult
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            return clone;
        }

        private TtResult MergeResultWith(ITtCloneable other)
        {
            var clone = new TtResult
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ErrorsList.AddRange(other.Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            clone.ConfirmationsList.AddRange(other.Confirmations);
            return clone;
        }
        #endregion

        #region Factory methods
        internal static ITwoTrack Ok() => new TtResult();

        internal static ITwoTrack Enclose(Func<ITwoTrack> func) => new TtResult().Do(func);
        

        internal static ITwoTrack Fail(TwoTrackError error)
        {
            return error is null
                ? new TtResult().AppendError(TwoTrackError.ArgumentNullError(2))
                : new TtResult().AppendError(error);
        }

        internal static ITwoTrack Fail(IEnumerable<TwoTrackError> errors)
        {
            var ttErrors = errors?.ToList();
            return errors is null || !ttErrors.Any()
                ? new TtResult().AppendError(TwoTrackError.ArgumentNullError(2))
                : new TtResult().AppendErrors(ttErrors);
        }

        internal static ITwoTrack Fail(ITtCloneable source, TwoTrackError error = default)
        {
            var clone = new TtResult
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
