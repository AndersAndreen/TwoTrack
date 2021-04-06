using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TwoTrackCore;

namespace TwoTrackUseCaseScenarioTests.FakesAndMocks
{
    internal class Logger
    {
        public void Log(IEnumerable<TwoTrackError> errors)
        {
            Console.WriteLine(string.Concat(errors.Select(error => error.ToString())));
            Debug.Print(string.Concat(errors.Select(error => error.ToString())));
        }

        public void Log(params TwoTrackError[] errors)
        {
            Console.WriteLine(string.Concat(errors.Select(error => error.ToString()))); ;
            Debug.Print(string.Concat(errors.Select(error => error.ToString())));
        }
    }
}
