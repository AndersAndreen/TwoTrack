using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericAddErrorsExtensions
    {
        public static ITwoTrack<T> AddErrors<T>(this ITwoTrack<T> source, ITwoTrack result) => source.AddErrors(result.Errors);
        public static ITwoTrack<T> AddErrors<T2, T>(this ITwoTrack<T> source, ITwoTrack<T2> result) => source.AddErrors(result.Errors);

        //public static ITwoTrack<T> AddConfirmations<T>(this ITwoTrack<T> source, ITwoTrack result) => source.AddConfirmations(result.Confirmations);
        //public static ITwoTrack<T> AddConfirmations<T2, T>(this ITwoTrack<T> source, ITwoTrack<T2> result) => source.AddConfirmations(result.Confirmations);
    }
}
