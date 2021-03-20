using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public abstract class TtResultBase<T> where T : TtResult
    {
        private readonly List<TtError> _errors = new List<TtError>();

        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;
        public IReadOnlyCollection<TtError> Errors => new List<TtError>(_errors);
        protected Func<Exception, bool> ExceptionFilter = ex => false;
        #region AddError base functionality

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
    }
}
