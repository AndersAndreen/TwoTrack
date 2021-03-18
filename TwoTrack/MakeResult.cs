namespace TwoTrack
{
    public class MakeResult
    {
        public static TtResult Ok() => new TtResult();

        public static TtResult Fail() => TtResult.Fail();

    }
}
