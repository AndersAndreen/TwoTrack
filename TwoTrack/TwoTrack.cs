using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public class TwoTrack
    {
        public static TtResult Ok() => TtResult.Ok();

        public static TtResult Fail() => TtResult.Ok().AddError(TtError.DefaultError());
        public static TtResult Fail(Exception exception) => TtResult.Ok().AddError(TtError.Exception(exception));
        public static TtResult Fail(IEnumerable<TtError> errors) => TtResult.Ok().AddErrors(errors);
        public static TtResult Fail(TtResult result) => TtResult.Ok().AddErrors(result);
    }
}
