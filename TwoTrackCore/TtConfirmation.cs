using System;

namespace TwoTrackCore
{
    public class TtConfirmation : ValueObject<TtConfirmation>
    {
        public ConfirmationLevel Level { get; private set; } = ConfirmationLevel.Information;
        public string Category { get; private set; }
        public string Description { get; private set; }
        private TtConfirmation() { }

        public static TtConfirmation Make(ConfirmationLevel confirmationLevel, string category, string description)
        {
            if (category is null || description is null) throw new ArgumentNullException();
            return new TtConfirmation
            {
                Level = confirmationLevel,
                Category = category,
                Description = description,
            };
        }

        // -------------------------------------------------------------------------------------------
        // Implementation of abstract methods
        // -------------------------------------------------------------------------------------------
        protected override bool ComparePropertiesForEquality(TtConfirmation confirmation)
        {
            return Level == confirmation.Level
                   && Category == confirmation.Category
                   && Description == confirmation.Description;
        }

        protected override string DefineToStringFormat()
            => $"ErrorLevel:{Level}, EventType:{Category}, Description:{Description}";
    }
}
