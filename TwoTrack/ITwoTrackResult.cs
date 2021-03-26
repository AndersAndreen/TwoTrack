using System;
using System.Collections.Generic;

namespace TwoTrackResult
{
    public interface ITwoTrackResult
    {
        bool Failed { get; }
        bool Succeeded { get; }
        IReadOnlyCollection<TtError> Errors { get; }
        IReadOnlyCollection<TtConfirmation> Confirmations { get; }
    }
}
