using Microsoft.AspNetCore.Mvc.ModelBinding;
using TwoTrackCore;
using TwoTrackExtensions.TryOut;

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
