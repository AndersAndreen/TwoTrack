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

        private TtError()
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

        #region Factory methods
        public static TtError Make(ErrorLevel errorLevel, string category, string description)
        {
            if (category is null || description is null) return ArgumentNullError();
            return new TtError
            {
                Level = errorLevel,
                Category = category,
                Description = description,
                StackTrace = Environment.StackTrace
            };
        }

        public static TtError Make(ErrorLevel errorLevel)
            => Make(errorLevel, "", ErrorDescriptions.DefaultError);

        public static TtError Exception(Exception exception)
            => exception == null
               ? ArgumentNullError()
               : new TtError
               {
                   Level = ErrorLevel.Error,
                   Category = ErrorCategory.Exception,
                   Description = $"{exception.GetType()}: {exception.Message}",
                   StackTrace = exception.StackTrace
               };

        public static TtError DefaultError()
            => Make(ErrorLevel.Error, ErrorCategory.Unspecified, ErrorDescriptions.DefaultError);


        public static TtError ArgumentNullError()
        {
            var callStack = new StackFrame(1, true);
            return new TtError
            {
                Level = ErrorLevel.Error,
                Category = ErrorCategory.ArgumentNullError,
                Description = $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}",
                StackTrace = Environment.StackTrace
            };
        }

        public static TtError ResultNullError()
        {
            var callStack = new StackFrame(1, true);
            return new TtError
            {
                Level = ErrorLevel.Error,
                Category = ErrorCategory.ResultNullError,
                Description = $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}",
                StackTrace = Environment.StackTrace
            };
        }

        public static TtError DesignBugError()
        {
            var callStack = new StackFrame(1, true);
            return new TtError
            {
                Level = ErrorLevel.Error,
                Category = ErrorCategory.DesignBugError,
                Description = $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}",
                StackTrace = Environment.StackTrace
            };
        }

        public static TtError ValidationError(string description)
        {
            return new TtError
            {
                Level = ErrorLevel.ReportError,
                Category = ErrorCategory.ValidationError,
                Description = description,
                StackTrace = Environment.StackTrace
            };
        }
        #endregion

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

