using Microsoft.AspNetCore.Mvc.ModelBinding;
using TwoTrackResult;
using TwoTrackResult.ExtensionMethods;

namespace DemoWebApp.Utils
{
    public static class TtResultGenericEndExtensions
    {
        public static T ValueAndModelState<T>(this ITwoTrack<T> source, ModelStateDictionary modelState, T defaultValue)
        {
            modelState.AddTwoTrackReports(source);
            return source.ValueOrDefault(defaultValue);

        }

    }
}
