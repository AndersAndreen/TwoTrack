using System;
using System.Diagnostics;
using TwoTrackCore.Defaults;

namespace TwoTrackCore
{
    public class TwoTrackError : TwoTrackErrorBase
    {
        private static TwoTrackError MakeError(ErrorLevel errorLevel, string category, string description, string stackTrace)
        {
            if (category is null || description is null) return MakeArgumentNullError();
            return new TwoTrackError
            {
                Level = errorLevel,
                Category = category,
                Description = description,
                StackTrace = stackTrace
            };
        }
        private static TwoTrackError MakeArgumentNullError() => MakeArgumentNullError(1);
        private static TwoTrackError MakeArgumentNullError(int skipFrames)
        {
            var callStack = new StackFrame(skipFrames, true);
            return MakeError(ErrorLevel.Error, ErrorCategory.ArgumentNullError, $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}", Environment.StackTrace);
        }

        public static TwoTrackError Error(ErrorLevel errorLevel, string category, string description)
            => MakeError(errorLevel, category, description, Environment.StackTrace);

        public static TwoTrackError Error(ErrorLevel errorLevel)
            => Error(errorLevel, "", ErrorDescriptions.DefaultError);

        public static TwoTrackError Exception(Exception exception)
            => exception == null
               ? ArgumentNullError()
               : MakeError(ErrorLevel.Error,
                   ErrorCategory.Exception,
                   $"{exception.GetType()}: {exception.Message}",
                   exception.StackTrace);

        public static TwoTrackError DefaultError()
            => Error(ErrorLevel.Error, ErrorCategory.Unspecified, ErrorDescriptions.DefaultError);

        public static TwoTrackError ArgumentNullError() => MakeArgumentNullError(2);
        public static TwoTrackError ArgumentNullError(int skipFrames) => MakeArgumentNullError(skipFrames);

        public static TwoTrackError ResultNullError()
        {
            var callStack = new StackFrame(1, true);
            return Error(ErrorLevel.Error,
                ErrorCategory.ResultNullError,
                $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}");
        }

        public static TwoTrackError ResultNullError(int skipFrames)
        {
            var callStack = new StackFrame(skipFrames, true);
            return Error(ErrorLevel.Error,
                ErrorCategory.ResultNullError,
                $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}");
        }
        public static TwoTrackError DesignBugError()
        {
            var callStack = new StackFrame(1, true);
            return Error(ErrorLevel.Error,
                 ErrorCategory.DesignBugError,
                 $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}");
        }

        public static TwoTrackError ValidationError(string description)
            => Error(ErrorLevel.ReportError, ErrorCategory.ValidationError, description);

        public static TwoTrackError ReportError(string category, string description)
            => Error(ErrorLevel.ReportError, category, description);
    }
}

