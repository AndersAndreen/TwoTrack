namespace TwoTrackCore.Defaults
{
    public class ErrorCategory
    {
        public static string Unspecified => nameof(Unspecified);
        public static string ArgumentNullError => nameof(ArgumentNullError);
        public static string DesignBugError => nameof(DesignBugError);
        public static string Exception => nameof(Exception);
        public static string ResultNullError => nameof(ResultNullError);
        public static string ValidationError => nameof(ValidationError);
    }
}
