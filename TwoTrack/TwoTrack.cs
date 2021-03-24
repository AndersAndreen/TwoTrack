using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public class TwoTrack
    {
        public static ITwoTrack Ok() => TtResult.Ok();

        public static ITwoTrack Fail() => TtResult.Fail(TtError.DefaultError());
        public static ITwoTrack Fail(Exception exception) => TtResult.Fail(TtError.Exception(exception));
        public static ITwoTrack Fail(IEnumerable<TtError> errors) => TtResult.Fail(errors);
        public static ITwoTrack Fail(ITwoTrack result) => TtResult.Fail(result.Errors);

        public static ITwoTrack<T> Fail<T>() => TtResult<T>.Fail(TtError.DefaultError());
        public static ITwoTrack<T> Fail<T>(Exception exception) => TtResult<T>.Fail(TtError.Exception(exception));
        public static ITwoTrack<T> Fail<T>(IEnumerable<TtError> errors) => TtResult<T>.Fail(errors);
        public static ITwoTrack<T> Fail<T>(ITwoTrack result) => TtResult<T>.Fail(result?.Errors);
        public static ITwoTrack<T> Fail<T>(ITwoTrack<T> result) => TtResult<T>.Fail(result?.Errors);

        public static ITwoTrack<T> Enclose<T>(Func<T> func) => TtResult<T>.Enclose(func);
        public static ITwoTrack<T> Enclose<T>(Func<ITwoTrack<T>> func) => TtResult<T>.Enclose(func);
    }                                
}
