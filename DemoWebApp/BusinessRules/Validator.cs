using System.Text.RegularExpressions;
using TwoTrackCore;

namespace DemoWebApp.BusinessRules
{
    public static class Validator
    {
        public static ITwoTrack ValidateIsbn(string isbnNumber)
            => Regex.IsMatch(isbnNumber, @"^[\d\s]*$")
                    ? TwoTrack.Ok()
                    : TwoTrack.Fail(TwoTrackError.ValidationError($"incorrect ISBN format: {isbnNumber}"));
        }
}

