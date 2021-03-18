using System.Collections.Generic;
using System.Linq;

namespace TwoTrack
{
    public class TtResult
    {
        public bool Failed => Errors.Any();
        public bool Succeeded => !Failed;
        private readonly List<object> _errors = new List<object>();

        public IReadOnlyCollection<object> Errors => new List<object>(_errors);

        internal static TtResult Fail()
        {
            var result = new TtResult();
            result._errors.Add(new object());
            return result;
        }


        internal TtResult()
        {
        }
    }
}
