using System;
using System.Diagnostics;
using TwoTrack.Core;

namespace TwoTrack.Extensions.TryOut
{
    public static class NonGenericValidateExtensions
    {
        public static ITwoTrack ValidateAlways(this ITwoTrack source, Func<bool> predicate, string errorDescription)
        {
            var result = source.Do(() => predicate()
                ? TwoTrack.Core.TwoTrack.Ok()
                : TwoTrack.Core.TwoTrack.Fail(TtError.ValidationError(errorDescription)).AddErrors(source.Errors));
            return result;
        }

        public static ITwoTrack ValidateAlways(this ITwoTrack source, Func<bool> predicate)
        {
            var callStack = new StackFrame(1, true);
            var result = source.Do(() => predicate()
                ? TwoTrack.Core.TwoTrack.Ok()
                : TwoTrack.Core.TwoTrack.Fail(TtError.ValidationError(predicate.Method.Name)).AddErrors(source.Errors));
            return result;
        }
    }
}
