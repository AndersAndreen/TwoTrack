using System;
using System.Diagnostics;
using TwoTrackCore;

namespace TwoTrackExtensions
{
    public static class NonGenericValidateExtensions
    {
        public static ITwoTrack ValidateAlways(this ITwoTrack source, Func<bool> predicate, string errorDescription)
        {
            var result = source.Do(() => predicate()
                ? TwoTrack.Ok()
                : TwoTrack.Fail(TwoTrackError.ValidationError(errorDescription)).AddErrors(source.Errors));
            return result;
        }

        public static ITwoTrack ValidateAlways(this ITwoTrack source, Func<bool> predicate)
        {
            var callStack = new StackFrame(1, true);
            var result = source.Do(() => predicate()
                ? TwoTrack.Ok()
                : TwoTrack.Fail(TwoTrackError.ValidationError(predicate.Method.Name)).AddErrors(source.Errors));
            return result;
        }
    }
}
