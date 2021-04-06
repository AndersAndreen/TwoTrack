using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoTrackCore.Internal
{
    internal abstract class TtResultBase<TChildClass> where TChildClass : TtResultBase<TChildClass>
    {
        protected readonly List<TwoTrackError> ErrorsList = new List<TwoTrackError>();
        protected readonly List<TtConfirmation> ConfirmationsList = new List<TtConfirmation>();


        public IReadOnlyCollection<TwoTrackError> Errors => new List<TwoTrackError>(ErrorsList);
        public IReadOnlyCollection<TtConfirmation> Confirmations => new List<TtConfirmation>(ConfirmationsList);

        public Func<Exception, bool> ExceptionFilter { get; protected set; } = ExceptionFilters.CatchNone;

        protected TtResultBase()
        {
        }

        #region Append errors and confirmations fluent style
        // These methods simplifies AddError and AddConfirmation methods
        protected TChildClass AppendError(TwoTrackError error)
        {
            error ??= TwoTrackError.ArgumentNullError();
            ErrorsList.Add(error);
            return (TChildClass)this;
        }

        protected TChildClass AppendErrors(IEnumerable<TwoTrackError> errors)
        {
            var ttErrors = errors?.ToList() ?? new List<TwoTrackError> { TwoTrackError.ArgumentNullError() }; ;
            ErrorsList.AddRange(ttErrors);
            return (TChildClass)this;
        }

        protected TChildClass AppendConfirmation(TtConfirmation confirmation)
        {
            ConfirmationsList.Add(confirmation);
            return (TChildClass)this;
        }

        protected TChildClass AppendConfirmations(IEnumerable<TtConfirmation> confirmations)
        {
            ConfirmationsList.AddRange(confirmations);
            return (TChildClass)this;
        }

        protected TChildClass SetExceptionFilter(Func<Exception, bool> exceptionFilter)
        {
            ExceptionFilter = exceptionFilter;
            return (TChildClass)this;
        }

        #endregion
    }
}
