//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TwoTrackCore.Defaults;

//namespace TwoTrackCore.Internal
//{
//    internal class TtResultAsync : TtResultBase<TtResult>, ITwoTrack
//    {
//        public bool Failed => Errors.Any(error => error.Level != ErrorLevel.Warning && error.Level != ErrorLevel.ReportWarning);
//        public bool Succeeded => !Failed;

//        private TtResultAsync()
//        {
//        }
//        #region Instance methods
//        public ITwoTrack AddError(TwoTrackError error) => TryCatch(() => Clone().AppendError(error)); //Todo: if exception add a designbug error also
//        public ITwoTrack AddErrors(IEnumerable<TwoTrackError> errors) => TryCatch(() => Clone().AppendErrors(errors)); //Todo: if exception add a designbug error also
//        public ITwoTrack ReplaceErrorsByCategory(string Category, TwoTrackError replacement)
//        {
//            if (!Errors.Any(error => error.Category == Category)) return this;

//            var clone = new TtResultAsync
//            {
//                ExceptionFilter = this.ExceptionFilter,
//            };
//            clone.ErrorsList.AddRange(Errors.Where(error => error.Category != ErrorCategory.ResultNullError).Concat(new[] { replacement }));
//            clone.ConfirmationsList.AddRange(Confirmations);
//            return clone;
//        }

//        public ITwoTrack AddConfirmation(TtConfirmation confirmation)
//        {
//            if (confirmation is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
//            return Failed
//                ? this
//                : TryCatch(() => Clone().AppendConfirmation(confirmation));
//        }

//        public ITwoTrack AddConfirmations(IEnumerable<TtConfirmation> confirmations)
//        {
//            if (confirmations is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
//            return Failed
//                ? this
//                : TryCatch(() => Clone().AppendConfirmations(confirmations));
//        }

//        public ITwoTrack SetExceptionFilter(Func<Exception, bool> exeptionFilter)
//        {
//            if (exeptionFilter is null) return Clone().AppendError(TwoTrackError.ArgumentNullError());
//            ExceptionFilter = exeptionFilter;
//            return this;
//        }

//        public async Task<ITwoTrack> DoAsync(Func<Task> task)
//        {
//            if (Failed) return this;
//            if (task is null) return new TtResultAsync().AppendError(TwoTrackError.ArgumentNullError());
//            return await new TtResultAsync().TryCatchAsync(async () =>
//            {
//                await task();
//                return this;
//            });
//        }

//        //public async Task<ITwoTrack> DoAsync(Func<Task<ITwoTrack>> task)
//        //{
//        //    if (Failed) return this;
//        //    if (task is null) return new TtResultAsync().AppendError(TwoTrackError.ArgumentNullError());
//        //    return await new TtResultAsync().AppendConfirmations(confirmations).TryCatchAsync(async () =>
//        //    {
//        //        var result = await task();
//        //        return new TT
//        //    });
//        //}


//        public ITwoTrack Do(Func<ITwoTrack> func)
//        {
//            if (Failed) return this;
//            if (func is null) return AddError(TwoTrackError.ArgumentNullError());
//            return TryCatch(func);
//        }

//        public ITwoTrack Do<T>(Func<ITwoTrack<T>> func)
//        {
//            if (Failed) return this;
//            if (func is null) return AddError(TwoTrackError.ArgumentNullError());
//            return TryCatch(() =>
//            {
//                var result = func();
//                return AddErrors(result.Errors);
//            });
//        }

//        private ITwoTrack TryCatch(Func<ITwoTrack> func)
//        {
//            try
//            {
//                return func();
//            }
//            catch (Exception e) when (ExceptionFilter(e))
//            {
//                return AddError(TwoTrackError.Exception(e));
//            }
//        }

//        private async Task<ITwoTrack> TryCatchAsync(Func<Task<ITwoTrack>> func)
//        {
//            try
//            {
//                return await func();
//            }
//            catch (Exception e) when (ExceptionFilter(e))
//            {
//                return AddError(TwoTrackError.Exception(e));
//            }
//        }

//        private TtResultAsync Clone()
//        {
//            var clone = new TtResultAsync
//            {
//                ExceptionFilter = this.ExceptionFilter,
//            };
//            clone.ErrorsList.AddRange(Errors);
//            clone.ConfirmationsList.AddRange(Confirmations);
//            return clone;
//        }
//        #endregion

//        #region Factory methods
//        internal static ITwoTrack Ok() => new TtResultAsync();

//        internal static ITwoTrack Enclose(Func<ITwoTrack> func) => new TtResultAsync().Do(func);
//        internal static ITwoTrack Enclose(Action action) => new TtResultAsync().Do(action);


//        internal static ITwoTrack Fail(TwoTrackError error)
//        {
//            return error is null
//                ? new TtResultAsync().AppendError(TwoTrackError.ArgumentNullError())
//                : new TtResultAsync().AppendError(error);
//        }

//        internal static ITwoTrack Fail(IEnumerable<TwoTrackError> errors)
//        {
//            var ttErrors = errors?.ToList();
//            return errors is null || !ttErrors.Any()
//                ? new TtResultAsync().AppendError(TwoTrackError.ArgumentNullError())
//                : new TtResultAsync().AppendErrors(ttErrors);
//        }

//        internal static ITwoTrack Fail(ITtCloneable source, TwoTrackError error = default)
//        {
//            var clone = new TtResultAsync
//            {
//                ExceptionFilter = source.ExceptionFilter,
//            };
//            clone.ErrorsList.AddRange(source.Errors);
//            clone.ConfirmationsList.AddRange(source.Confirmations);
//            if (source.Succeeded) return clone.AppendError(error ?? TwoTrackError.DefaultError());
//            return error is null
//                ? clone
//                : clone.AddError(error);
//        }
//        #endregion

//        #region Async Factory Methods
//        public static async Task<ITwoTrack> EncloseAsync(Func<Task> task) => new TtResultAsync().DoAsync(task);

//        #endregion
//    }
//}
