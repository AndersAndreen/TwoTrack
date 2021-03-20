using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public class TwoTrack
    {
        public static TtResult Ok() => new TtResult();

        public static TtResult Fail() => new TtResult().AddError(TtError.DefaultError());
        public static TtResult Fail(Exception exception) => new TtResult().AddError(TtError.Exception(exception));
        public static TtResult Fail(IEnumerable<TtError> errors) => new TtResult().AddErrors(errors);
        public static TtResult Fail(TtResult result) => new TtResult().AddErrors(result);
    }
}
