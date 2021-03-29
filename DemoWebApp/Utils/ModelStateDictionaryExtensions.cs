using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TwoTrack.Core;
using TwoTrack.Core.Defaults;

namespace DemoWebApp.Utils
{
    public static class ModelStateDictionaryExtensions
    {
        public static ModelStateDictionary AddTwoTrackReports(this ModelStateDictionary modelState, ITwoTrackResult result)
        {
            result.Confirmations
                .Where(confirmation => confirmation.Level == ConfirmationLevel.Report)
                .ToList()
                .ForEach(error => modelState.AddModelError(ConfirmationLevel.Report.ToString(), error.Description));
            result.Errors
                .Where(error=>error.Level == ErrorLevel.ReportWarning)
                .ToList()
                .ForEach(error=> modelState.AddModelError(ErrorLevel.ReportWarning.ToString(), error.Description));
            result.Errors
                .Where(error => error.Level == ErrorLevel.ReportError)
                .ToList()
                .ForEach(error => modelState.AddModelError(ErrorLevel.ReportError.ToString(), error.Description));
            if (modelState.IsValid && result.Failed) modelState.AddTwoTrackError(ErrorDescriptions.UnspecifiedError);

            return modelState;
        }

        public static ModelStateDictionary AddTwoTrackConfirmation(this ModelStateDictionary modelState, string message)
        {
            modelState.AddModelError(ConfirmationLevel.Report.ToString(), message);
            return modelState;
        }

        public static ModelStateDictionary AddTwoTrackWarning(this ModelStateDictionary modelState, string message)
        {
            modelState.AddModelError(ErrorLevel.ReportWarning.ToString(), message);
            return modelState;
        }

        public static ModelStateDictionary AddTwoTrackError(this ModelStateDictionary modelState, string message)
        {
            modelState.AddModelError(ErrorLevel.ReportError.ToString(), message);
            return modelState;
        }

        public static IReadOnlyCollection<string> GetTwoTrackConfirmations(this ModelStateDictionary modelState)
            => modelState.GetMessagess(ConfirmationLevel.Report.ToString());
        
        public static IReadOnlyCollection<string> GetTwoTrackWarnings(this ModelStateDictionary modelState)
            => modelState.GetMessagess(ErrorLevel.ReportWarning.ToString());

        public static IReadOnlyCollection<string> GetTwoTrackErrors(this ModelStateDictionary modelState)
            => modelState.GetMessagess(ErrorLevel.ReportError.ToString());

        private static IReadOnlyCollection<string> GetMessagess(this ModelStateDictionary modelState, string key)
            => modelState.Where(m => m.Key == key)
                .Select(m => m.Value.Errors.Select(e => e.ErrorMessage).ToList())
                .FirstOrDefault() ?? new List<string>();
    }
}
