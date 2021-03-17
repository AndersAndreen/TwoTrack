using System;
using System.Collections.Generic;
using System.Text;

namespace Railcar
{
    public class MakeResult
    {
        public static RailcarResult Ok() => new RailcarResult();

        public static RailcarResult Fail() => RailcarResult.Fail();

    }
}
