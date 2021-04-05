using System;
using System.Diagnostics;
using TwoTrackCore.Defaults;

namespace TwoTrackCore
{
    public class TwoTrackError : TtError
    {
        public static TtError Error(ErrorLevel errorLevel, string category, string description)
            => TtError.MakeError(errorLevel, category, description, Environment.StackTrace);

        public static TtError Error(ErrorLevel errorLevel)
            => Error(errorLevel, "", ErrorDescriptions.DefaultError);

        public static TtError Exception(Exception exception)
            => exception == null
               ? ArgumentNullError()
               : MakeError(ErrorLevel.Error,
                   ErrorCategory.Exception,
                   $"{exception.GetType()}: {exception.Message}",
                   exception.StackTrace);

        public static TtError DefaultError()
            => Error(ErrorLevel.Error, ErrorCategory.Unspecified, ErrorDescriptions.DefaultError);

        public static TtError ArgumentNullError() => TtError.MakeArgumentNullError();

        public static TtError ResultNullError()
        {
            var callStack = new StackFrame(1, true);
            return Error(ErrorLevel.Error,
                ErrorCategory.ResultNullError,
                $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}");
        }

        public static TtError DesignBugError()
        {
            var callStack = new StackFrame(1, true);
            return Error(ErrorLevel.Error,
                 ErrorCategory.DesignBugError,
                 $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}");
        }

        public static TtError ValidationError(string description)
        {
            return Error(ErrorLevel.ReportError,
                 ErrorCategory.ValidationError,
                 description);
        }

    }
}

