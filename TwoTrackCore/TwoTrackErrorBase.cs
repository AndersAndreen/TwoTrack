namespace TwoTrackCore
{
    public abstract class TwoTrackErrorBase : ValueObject<TwoTrackErrorBase>
    {
        /// <summary>
        /// ErrorLevel defines the severity of the error. See detailed description of the different levels at <see cref="ErrorLevel"/>.
        /// </summary>
        public ErrorLevel Level { get; protected set; } = ErrorLevel.Error;
        /// <summary>
        /// Category is a genesal identifier that can be used for many differend purpouses: 
        /// When logging it can be useed to group errors.
        /// When sending messages to a view (via ModelState) it can hold the ID-reference that Modelstate uses.
        /// When used in a webb API it can be used to hold HTTP-response codes.
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        /// Description may hold a resource key or an error message in a MVC-app, an internal error code when logging
        /// or an API-related error code when used in an API.
        /// </summary>
        public string Description { get; protected set; }

        public string StackTrace { get; protected set; }

        protected TwoTrackErrorBase()
        {

        }


        #region Implementation of abstract methods
        protected override bool ComparePropertiesForEquality(TwoTrackErrorBase error)
        {
            return Level == error.Level
                   && Category == error.Category
                   && Description == error.Description
                   && StackTrace == error.StackTrace;
        }

        protected override string DefineToStringFormat()
            => $"ErrorLevel:{Level}, EventType:{Category}, Description:{Description}";
        #endregion
    }
}

