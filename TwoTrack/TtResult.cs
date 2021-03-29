using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoTrackResult
{
    internal class TtResult : TtResultBase<TtResult>, ITwoTrack
    {
        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;

        private TtResult()
        {
        }

        public ITwoTrack AddError(TtError error) => Clone().AppendError(error);
        public ITwoTrack AddErrors(IEnumerable<TtError> errors) => Clone().AppendErrors(errors);
        public ITwoTrack AddConfirmation(TtConfirmation confirmation) => Clone().AppendConfirmation(confirmation);
        public ITwoTrack AddConfirmations(IEnumerable<TtConfirmation> confirmations) => Clone().AppendConfirmations(confirmations);

        public ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter)
        {
            if (exeptionFilter is null) return Clone().AppendError(TtError.ArgumentNullError());
            ExceptionFilter = exeptionFilter;
            return this;
        }

        public ITwoTrack Do(Action action)
        {
            if (action is null) return AddError(TtError.ArgumentNullError());
            if (Failed) return this;
            return TryCatch(()=>
            {
                action();
                return this;
            });
        }

        public ITwoTrack Do(Func<ITwoTrack> func)
        {
            if (func is null) return AddError(TtError.ArgumentNullError());
            return Failed ? this : TryCatch(func);
        }

        public ITwoTrack Do<T>(Func<ITwoTrack<T>> func)
        {
            if (func is null) return AddError(TtError.ArgumentNullError());
            if (Failed) return this;
            return TryCatch(() =>
            {
                var result = func();
                return AddErrors(result.Errors);
            });
        }

        private ITwoTrack TryCatch(Func<ITwoTrack> func)
        {
            if (Failed) return this;
            try
            {
                return func();
            }
            catch (Exception e) when (ExceptionFilter(e))
            {
                return Clone().AppendError(TtError.Exception(e));
            }
        }

        private TtResult Clone()
        {
            var clone =new TtResult
            {
                ExceptionFilter = this.ExceptionFilter,
            };
            clone.ErrorsList.AddRange(Errors);
            clone.ConfirmationsList.AddRange(Confirmations);
            return clone;
        }

        #region Factory methods
        internal static ITwoTrack Ok() => new TtResult();

        internal static ITwoTrack Fail(TtError error)
        {
            return error is null
                ? new TtResult().AppendError(TtError.ArgumentNullError())
                : new TtResult().AppendError(error);
        }

        internal static ITwoTrack Fail(IEnumerable<TtError> errors)
        {
            var ttErrors = errors?.ToList();
            return errors is null || !ttErrors.Any()
                ? new TtResult().AppendError(TtError.ArgumentNullError())
                : new TtResult().AppendErrors(ttErrors);
        }

        internal static ITwoTrack Fail(ITtCloneable source, TtError error = default)
        {
            var clone = new TtResult
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
