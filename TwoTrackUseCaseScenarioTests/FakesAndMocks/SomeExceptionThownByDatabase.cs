using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class SomeExceptionThownByDatabase : Exception
    {
        public SomeExceptionThownByDatabase()
        {
        }

        public SomeExceptionThownByDatabase(string message)
            : base(message)
        {
        }

        public SomeExceptionThownByDatabase(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
