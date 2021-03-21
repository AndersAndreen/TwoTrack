using System;
using System.Diagnostics;
using TwoTrackResult.Defaults;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public class TtError : ValueObject<TtError>
    {
        public ErrorLevel Level { get; private set; } = ErrorLevel.Error;
        public string Category { get; private set; }
        public string Description { get; private set; }
        public string StackTrace { get; private set; }
        // -------------------------------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------------------------------

        private TtError()
        {
        }

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
                   Category = Defaults.Category.Exception,
                   Description = $"{exception.GetType()}: {exception.Message}",
                   StackTrace = exception.StackTrace
               };

        public static TtError DefaultError()
            => Make(ErrorLevel.Error, "", ErrorDescriptions.DefaultError);


        public static TtError ArgumentNullError()
        {
            StackFrame callStack = new StackFrame(1, true);
            return new TtError
            {
                Level = ErrorLevel.Error,
                Category = Defaults.Category.ArgumentNullError,
                Description = $"At {callStack.GetFileName()}, line {callStack.GetFileLineNumber()}",
                StackTrace = Environment.StackTrace
            };
        }

        // -------------------------------------------------------------------------------------------
        // Implementation of abstract methods
        // -------------------------------------------------------------------------------------------
        protected override bool ComparePropertiesForEquality(TtError error)
        {
            return Level == error.Level
                   && Category == error.Category
                   && Description == error.Description
                   && StackTrace == error.StackTrace;
        }

        protected override string DefineToStringFormat()
            => $"ErrorLevel:{Level}, EventType:{Category}, Description:{Description}";
    }
}

