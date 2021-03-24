using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public abstract class TtResultBase<T> where T : TtResultBase<T>
    {
        private readonly List<TtError> _errors = new List<TtError>();
        private readonly List<TtConfirmation> _confirmations = new List<TtConfirmation>();

        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;
        public IReadOnlyCollection<TtError> Errors => new List<TtError>(_errors);
        public IReadOnlyCollection<TtConfirmation> Confirmations => new List<TtConfirmation>(_confirmations);

        public Func<Exception, bool> ExceptionFilter { get; protected set; } = (ex => false);
        #region AddError
        protected T AddError(T result, TtError error)
        {
            error = error ?? TtError.ArgumentNullError();
            result._errors.AddRange(Errors);
            result._errors.Add(error);
            return result;
        }

        protected T AddErrors(T result, IEnumerable<TtError> errors)
        {
            errors = errors ?? TtError.ArgumentNullError().ToList();
            result._errors.AddRange(Errors);
            result._errors.AddRange(errors);
            return result;
        }
        #endregion

        #region AddConfirmation
        protected T AddConfirmation(T result, TtConfirmation confirmation)
        {
            if (result is null) throw new ArgumentNullException(nameof(result));
            if (confirmation is null) return AddErrors(result, Errors);
            result._confirmations.AddRange(Confirmations);
            result._confirmations.Add(confirmation);
            return result;
        }

        protected T AddConfirmations(T result, IEnumerable<TtConfirmation> confirmations)
        {
            if (result is null) throw new ArgumentNullException(nameof(result));
            if (confirmations is null) return AddErrors(result, Errors);
            result._confirmations.AddRange(Confirmations);
            result._confirmations.AddRange(confirmations);
            return result;
        }
        #endregion
    }
}
