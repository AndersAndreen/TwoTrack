using System.Collections.Generic;
using System.Linq;

namespace TwoTrackResult
{
    public class TtResult : TtResultBase
    {


        internal TtResult()
        {
        }

        internal static TtResult Fail()
        {
            var result = new TtResult();
            return result.AddError(TtError.Make(ErrorLevel.Error));
        }



    }
}
