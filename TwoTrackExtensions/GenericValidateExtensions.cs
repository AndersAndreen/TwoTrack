using System;
using System.Diagnostics;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class GenericValidateExtensions
    {
        public static ITwoTrack<T> ValidateAlways<T>(this ITwoTrack<T> source, Func<T, bool> predicate)
        {
            var callStack = new StackFrame(1, true);
            var result = source.Do(value => predicate(value)
                ? TwoTrack.Ok()
                : TwoTrack.Fail(TwoTrackError.ValidationError(predicate.Method.Name)).AddErrors(source.Errors));
            return result;
        }

        public static ITwoTrack<T> ValidateAlways<T>(this ITwoTrack<T> source, Func<T, bool> predicate, string message)
        {
            var callStack = new StackFrame(1, true);
            var result = source.Do(value
                => predicate(value)
                    ? TwoTrack.Ok()
                    : TwoTrack.Fail(TwoTrackError.ValidationError(message)).AddErrors(source.Errors));
            return result;
        }
    }
}
