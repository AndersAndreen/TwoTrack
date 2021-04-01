namespace TwoTrackExtensions
{
    public static class GenericReplaceErrorExtensions
    {
        //public static ITwoTrack<T> ReplaceNullResultWithErrorMessage<T>(this ITwoTrack<T> source, string description)
        //{
        //    if (source.Succeeded) return source;
        //    var errors = source.Errors.Where(error => error.Category != ErrorCategory.ResultNullError).ToList();
        //    var replacedErrors = source.Errors
        //        .Where(error => error.Category == ErrorCategory.ResultNullError)
        //        .Select(er => er.Convert(ErrorLevel.ReportError, description));
        //    errors.AddRange(replacedErrors);
        //    return TwoTrack.Fail<T>(errors);
        //}
    }
}
