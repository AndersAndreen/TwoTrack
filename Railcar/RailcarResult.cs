using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Railcar
{
    public class RailcarResult
    {
        public bool Failed => Errors.Any();
        public bool Succeeded => !Failed;
        private readonly List<object> _errors = new List<object>();

        public IReadOnlyCollection<object> Errors => new List<object>(_errors);

        internal static RailcarResult Fail()
        {
            var result = new RailcarResult();
            result._errors.Add(new object());
            return result;
        }


        internal RailcarResult()
        {
        }
    }
}
