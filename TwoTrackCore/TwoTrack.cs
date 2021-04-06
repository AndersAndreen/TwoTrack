using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwoTrackCore.Internal;

namespace TwoTrackCore
{
    public class TwoTrack
    {
        public static ITwoTrack Ok() => TtResult.Ok();

        public static ITwoTrack Fail(Exception exception) => TtResult.Fail(TwoTrackError.Exception(exception));
        public static ITwoTrack Fail(TwoTrackError error) => TtResult.Fail(error);
        public static ITwoTrack Fail(ITtCloneable result, TwoTrackError error = default) => TtResult.Fail(result, error);

        public static ITwoTrack<T> Fail<T>(Exception exception) => TtResult<T>.Fail(TwoTrackError.Exception(exception));
        public static ITwoTrack<T> Fail<T>(TwoTrackError error) => TtResult<T>.Fail(error);
        public static ITwoTrack<T> Fail<T>(ITtCloneable result, TwoTrackError error = default) => TtResult<T>.Fail(result, error);

        public static ITwoTrack Enclose(Action action) => TtResult.Enclose(action);
        public static ITwoTrack Enclose(Func<ITwoTrack> func) => TtResult.Enclose(func);
        public static ITwoTrack<T> Enclose<T>(Func<T> func) => TtResult<T>.Enclose(func);
        public static ITwoTrack<T> Enclose<T>(Func<ITwoTrack<T>> func) => TtResult<T>.Enclose(func);


        //public static async Task<ITwoTrack> EncloseAsync(Func<Task> task) => await TtResult.EncloseAsync(task);


    }
}
