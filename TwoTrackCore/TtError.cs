using System;
using System.Diagnostics;
using TwoTrackCore.Defaults;

namespace TwoTrackCore
{
    public class TtError : ValueObject<TtError>
    {
        /// <summary>
        /// ErrorLevel defines the severity of the error. See detailed description of the different levels at <see cref="ErrorLevel"/>.
        /// </summary>
        public ErrorLevel Level { get; private set; } = ErrorLevel.Error;
        /// <summary>
        /// Category is a genesal identifier that can be used for many differend purpouses: 
        /// When logging it can be useed to group errors.
        /// When sending messages to a view (via ModelState) it can hold the ID-reference that Modelstate uses.
        /// When used in a webb API it can be used to hold HTTP-response codes.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Description may hold a resource key or an error message in a MVC-app, an internal error code when logging
        /// or an API-related error code when used in an API.
        /// </summary>
        public string Description { get; private set; }

        public string StackTrace { get; private set; }

        protected TtError()
        {
        }

        internal TtError Convert(ErrorLevel errorLevel, string description)
        {
            return new TtError
            {
                Level = errorLevel,
                Category = Category,
                Description = description,
                StackTrace = StackTrace
            };
        }

        protected static TtError MakeError(ErrorLevel errorLevel, string category, string description, string stackTrace)
        {
            if (category is null || description is null) return MakeArgumentNullError();
            return new TtError
            {
                Level = errorLevel,
                Category = category,
                Description = description,
                StackTrace = stackTrace
            };
        }
        protected static TtError MakeArgumentNullError()
        {
            var callStack = new StackFrame(1, true);
            return MakeError(ErrorLevel.Error, ErrorCategory.ArgumentNullError, $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}", Environment.StackTrace);
        }

        #region Implementation of abstract methods
        protected override bool ComparePropertiesForEquality(TtError error)
        {
            return Level == error.Level
                   && Category == error.Category
                   && Description == error.Description
                   && StackTrace == error.StackTrace;
        }

        protected override string DefineToStringFormat()
            => $"ErrorLevel:{Level}, EventType:{Category}, Description:{Description}";
        #endregion
    }
}

