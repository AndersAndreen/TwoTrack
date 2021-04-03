using System;
using System.Collections.Generic;
using System.Text;
using TwoTrackCore;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class Logger
    {
        public void Log(IEnumerable<TtError> errors)
        {

        }

        public void Log(params TtError[] errors)
        {

        }
    }
}
