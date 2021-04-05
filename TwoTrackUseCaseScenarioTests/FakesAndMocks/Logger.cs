using System;
using System.Collections.Generic;
using System.Text;
using TwoTrackCore;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class Logger
    {
        public void Log(IEnumerable<TwoTrackError> errors)
        {

        }

        public void Log(params TwoTrackError[] errors)
        {

        }
    }
}
