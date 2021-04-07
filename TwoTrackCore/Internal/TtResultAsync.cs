//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TwoTrackCore.Defaults;

//namespace TwoTrackCore.Internal
//{
//    internal class TtResultAsync : TtResult, ITwoTrack
//    {
//        private TtResultAsync()
//        {
//        }

//        #region Public instance methods
//        public async Task<ITwoTrack> DoAsync(Func<Task<ITwoTrack>> task)
//        {
//            if (Failed) return this;
//            if (task is null) return AddError(TwoTrackError.ArgumentNullError());
//            try
//            {
//                var result = await task();
//                return MergeResultWith(result);
//            }
//            catch (Exception e) when (ExceptionFilter(e))
//            {
//                return AddError(TwoTrackError.Exception(e));
//            }
//        }

//        public async Task<ITwoTrack> DoAsync(Func<Task> task)
//        {
//            if (Failed) return this;
//            if (task is null) return AddError(TwoTrackError.ArgumentNullError());
//            try
//            {
//                await task();
//            }
//            catch (Exception e) when (ExceptionFilter(e))
//            {
//                return AddError(TwoTrackError.Exception(e));
//            }
//            return this;
//        }
//        #endregion



//        #region Factory methods



//        #endregion
//    }
//}
