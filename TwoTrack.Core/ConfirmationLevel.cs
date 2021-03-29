namespace TwoTrack.Core
{
    public enum ConfirmationLevel
    {
        /// <summary>
        /// Green light:
        /// Confirmations that are used for interactive investigation during development.
        /// Should primarily contain information useful for debugging and have no long-term value.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Green light:
        /// Confirmations that track the general flow of the application. Should have long-term value.
        /// </summary>
        Information = 2,

        /// <summary>
        /// Green light:
        /// This ConfirmationLevel is purely for user feedback or API responses and will not be logged. 
        /// </summary>
        Report = 20
    }
}

