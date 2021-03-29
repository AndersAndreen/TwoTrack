using System.Collections.Generic;

namespace TwoTrack.Core
{
    public interface ITwoTrackResult
    {
        bool Failed { get; }
        bool Succeeded { get; }
        IReadOnlyCollection<TtError> Errors { get; }
        IReadOnlyCollection<TtConfirmation> Confirmations { get; }
    }
}
