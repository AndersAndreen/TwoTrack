using System;
using System.Collections.Generic;
using TwoTrack.Core.Internal;

namespace TwoTrack.Core
{
    public class TwoTrack
    {
        public static ITwoTrack Ok() => TtResult.Ok();

        public static ITwoTrack Fail() => TtResult.Fail(TtError.DefaultError());
        public static ITwoTrack Fail(Exception exception) => TtResult.Fail(TtError.Exception(exception));
        public static ITwoTrack Fail(TtError error) => TtResult.Fail(error);
        public static ITwoTrack Fail(IEnumerable<TtError> errors) => TtResult.Fail(errors);
        public static ITwoTrack Fail(ITtCloneable result, TtError error = default) => TtResult.Fail(result, error);

        public static ITwoTrack<T> Fail<T>() => TtResult<T>.Fail(TtError.DefaultError());
        public static ITwoTrack<T> Fail<T>(Exception exception) => TtResult<T>.Fail(TtError.Exception(exception));
        public static ITwoTrack<T> Fail<T>(TtError error) => TtResult<T>.Fail(error);
        public static ITwoTrack<T> Fail<T>(IEnumerable<TtError> errors) => TtResult<T>.Fail(errors);
        public static ITwoTrack<T> Fail<T>(ITtCloneable result, TtError error = default) => TtResult<T>.Fail(result, error);

        public static ITwoTrack<T> Enclose<T>(Func<T> func) => TtResult<T>.Enclose(func);
        public static ITwoTrack<T> Enclose<T>(Func<ITwoTrack<T>> func) => TtResult<T>.Enclose(func);
        public static ITwoTrack<T> Enclose<T>(Func<T> func, Func<T, bool> validator, TtError errorIfFail) => TtResult<T>.Enclose(func, validator);

    }
}
