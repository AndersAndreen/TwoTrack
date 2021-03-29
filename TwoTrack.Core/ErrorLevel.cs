namespace TwoTrack.Core
{
    public enum ErrorLevel
    {
        /// <summary>
        /// Yellow light
        /// Highlights an abnormal or unexpected event in the application flow,
        /// but do not otherwise cause the application execution to stop.
        /// Warnings do not cause AppResult fail.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Red light
        /// Errors that highlight when the current flow of execution is stopped due to a failure,
        /// For example if a context-dependent exception was thrown or if a data request failed.
        /// These should indicate a failure in the current activity, not an application-wide failure.
        /// Errors cause AppResult fail
        /// </summary>
        Error = 4,

        /// <summary>
        /// Red light
        /// Logs that describe an unrecoverable application or system crash,
        /// or a catastrophic failure that requires immediate attention.
        /// </summary>
        Critical = 5,

        /// <summary>
        /// Yellow light
        /// This ErrorLevel is purely for user feedback or API responses and will not be logged.
        /// ReportWarnings do not cause AppResult fail. 
        /// </summary>
        ReportWarning = 23,

        /// <summary>
        /// Red light
        /// This ErrorLevel is purely for user feedback or API responses and will not be logged.
        /// It makes AppResult
        /// Reports cause AppResult fail.
        /// </summary>
        ReportError = 24
    }
}
