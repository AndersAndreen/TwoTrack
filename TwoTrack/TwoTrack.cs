using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public class TwoTrack
    {
        public static TtResult Ok() => TtResult.Ok();

        public static TtResult Fail() => TtResult.Fail(TtError.DefaultError());
        public static TtResult Fail(Exception exception) => TtResult.Fail(TtError.Exception(exception));
        public static TtResult Fail(IEnumerable<TtError> errors) => TtResult.Fail(errors);
        public static TtResult Fail(TtResult result) => TtResult.Fail(result.Errors);

        public static TtResult<T> Fail<T>() => TtResult<T>.Fail(TtError.DefaultError());
        public static TtResult<T> Fail<T>(Exception exception) => TtResult<T>.Fail(TtError.Exception(exception));
        public static TtResult<T> Fail<T>(IEnumerable<TtError> errors) => TtResult<T>.Fail(errors);
        public static TtResult<T> Fail<T>(TtResult result) => TtResult<T>.Fail(result?.Errors);
    }                                
}
