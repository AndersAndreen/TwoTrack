using System;
using System.Reflection;
using TwoTrackResult.Defaults;
using TwoTrackResult.Utilities;

namespace TwoTrackResult
{
    public class TtError
    {
        public ErrorLevel Level { get; private set; } = ErrorLevel.Error;
        public string Category { get; private set; }
        public string Description { get; private set; }
        public string StackTrace { get; private set; }
        // -------------------------------------------------------------------------------------------
        // Constructors
        // -------------------------------------------------------------------------------------------


        //private TtError(Exception exception)
        //    : this(Category.Exception, ErrorLevel.Error, exception.Message)
        //{
        //    Exception = exception;
        //    StackTrace = exception.StackTrace;
        //}

        private TtError()
        {
        }

        public static TtError Make(ErrorLevel errorLevel, string category, string description)
        {
            if (category is null || description is null) return MakeArgumentNullError(typeof(TtError), MethodBase.GetCurrentMethod());
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


        public static TtError MakeArgumentNullError(object caller, MethodBase methodBase)
            => Make(ErrorLevel.Error, ErrorCategory.ArgumentNullError, Meta.GetMethodSignature(caller, methodBase));

    }
}
