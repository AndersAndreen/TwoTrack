using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TwoTrackResult
{
    public abstract class TtResultBase
    {
        private readonly List<TtError> _errors = new List<TtError>();

        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
        public bool Succeeded => !Failed;
        public IReadOnlyCollection<TtError> Errors => new List<TtError>(_errors);

        public TtResult AddError(TtError error)
        {
            var result = new TtResult();
            result._errors.AddRange(Errors);
            result._errors.Add(error ?? TtError.MakeArgumentNullError(this, MethodBase.GetCurrentMethod()));
            return result;
        }

        public TtResult AddErrors(IEnumerable<TtError> errors)
        {
            var result = new TtResult();
            result._errors.AddRange(Errors);
            result._errors.AddRange(errors ?? new List<TtError> { TtError.MakeArgumentNullError(this, MethodBase.GetCurrentMethod()) });
            return result;
        }

    }
}
