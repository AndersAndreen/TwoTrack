using System.Collections.Generic;

namespace TwoTrackCore
{
    public interface ITwoTrackResult
    {
        bool Failed { get; }
        bool Succeeded { get; }
        IReadOnlyCollection<TwoTrackError> Errors { get; }
        IReadOnlyCollection<TtConfirmation> Confirmations { get; }
    }
}
