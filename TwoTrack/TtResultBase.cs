﻿using System;
using System.Collections.Generic;
using System.Linq;
using TwoTrackResult.Utilities;
using System.Runtime.CompilerServices;

namespace TwoTrackResult
{
    public abstract class TtResultBase<TChildClass> where TChildClass : TtResultBase<TChildClass>
    {
        protected readonly List<TtError> ErrorsList = new List<TtError>();
        protected readonly List<TtConfirmation> ConfirmationsList = new List<TtConfirmation>();


        public IReadOnlyCollection<TtError> Errors => new List<TtError>(ErrorsList);
        public IReadOnlyCollection<TtConfirmation> Confirmations => new List<TtConfirmation>(ConfirmationsList);

        public Func<Exception, bool> ExceptionFilter { get; protected set; } = (ex => false);

        protected TtResultBase()
        {
        }

        #region Append errors and confirmations fluent style
        // These methods simplifies AddError and AddConfirmation methods
        protected TChildClass AppendError(TtError error)
        {
            error ??= TtError.ArgumentNullError();
            ErrorsList.Add(error);
            return (TChildClass)this;
        }

        protected TChildClass AppendErrors(IEnumerable<TtError> errors)
        {
            var ttErrors = errors?.ToList() ?? TtError.ArgumentNullError().ValueToList();
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
        #endregion

    }
}
