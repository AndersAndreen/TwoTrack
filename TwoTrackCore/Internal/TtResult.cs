using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoTrackCore.Internal
{
    internal class TtResult : TtResultBase<TtResult>, ITwoTrack
    {
        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;

        private TtResult()
        {
        }

        public ITwoTrack AddError(TwoTrackError error) => TryCatch(() => Clone().AppendError(error)); //Todo: if exception add a designbug error also
        public ITwoTrack AddErrors(IEnumerable<TwoTrackError> errors) => TryCatch(() => Clone().AppendErrors(errors)); //Todo: if exception add a designbug error also
        public ITwoTrack AddConfirmation(TtConfirmation confirmation)
        {
            if (confirmation is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
            return Failed
                ? this
                : TryCatch(() => Clone().AppendConfirmation(confirmation));
        }

        public ITwoTrack AddConfirmations(IEnumerable<TtConfirmation> confirmations)
        {
            if (confirmations is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
            return Failed
                ? this
                : TryCatch(() => Clone().AppendConfirmations(confirmations));
        }

        public ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public ITwoTrack Do(Action action)
        {
            if (Failed) return this;
            if (action is null) return AddError(TwoTrackError.ArgumentNullError());
            return TryCatch(() =>
            {
                action();
                return this;
            });
        }

        public ITwoTrack Do(Func<ITwoTrack> func)
        {
            if (Failed) return this;
            if (func is null) return AddError(TwoTrackError.ArgumentNullError());
            return TryCatch(func);
        }

        public ITwoTrack Do<T>(Func<ITwoTrack<T>> func)
        {
            if (Failed) return this;
            if (func is null) return AddError(TwoTrackError.ArgumentNullError());
            return TryCatch(() =>
            {
                var result = func();
                return AddErrors(result.Errors);
            });
        }

        private ITwoTrack TryCatch(Func<ITwoTrack> func)
        {
            try
            {
                return func();
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return AddError(TwoTrackError.Exception(e));
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

        #region Factory methods
        internal static ITwoTrack Ok() => new TtResult();

        internal static ITwoTrack Enclose(Func<ITwoTrack> func) => new TtResult().Do(func);
        internal static ITwoTrack Enclose(Action action) => new TtResult().Do(action);


        internal static ITwoTrack Fail(TwoTrackError error)
        {
            return error is null
                ? new TtResult().AppendError(TwoTrackError.ArgumentNullError())
                : new TtResult().AppendError(error);
        }

        internal static ITwoTrack Fail(IEnumerable<TwoTrackError> errors)
        {
            var ttErrors = errors?.ToList();
            return errors is null || !ttErrors.Any()
                ? new TtResult().AppendError(TwoTrackError.ArgumentNullError())
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
