using System.Collections.Generic;
using System.Linq;

namespace TwoTrackResult
{
    public class TtResult
    {
        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;
        private readonly List<TtError> _errors = new List<TtError>();

        public IReadOnlyCollection<TtError> Errors => new List<TtError>(_errors);

        internal TtResult()
        {
        }

        internal static TtResult Fail()
        {
            var result = new TtResult();
            result._errors.Add(TtError.Make(ErrorLevel.Error));
            return result;
        }

        public TtResult AddError(TtError error)
        {
            var result = new TtResult();
            result._errors.AddRange(Errors);
            result._errors.Add(error ?? TtError.MakeArgumentNullError());
            return result;
        }
    }
}
