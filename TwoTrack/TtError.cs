namespace TwoTrackResult
{
    public class TtError
    {
        public ErrorLevel Level { get; private set; } = ErrorLevel.Error;

        private TtError()
        {
        }

        public static TtError Make(ErrorLevel errorLevel)
        {
            return new TtError { Level = errorLevel };
        }

    }
}
