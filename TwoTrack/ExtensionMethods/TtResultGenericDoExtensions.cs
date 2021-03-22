﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult
{
    public static class TtResultGenericDoExtensions
    {
        public static TtResult<T> Do<T>(this TtResult<T> resultIn, Action action)
        {
            if (action is null) return resultIn.AddError(TtError.ArgumentNullError());
            if (resultIn.Failed) return resultIn;
            try
            {
                action();
                return resultIn;
            }
            catch (Exception e) when (resultIn.ExceptionFilter(e))
            {
                return resultIn.AddError(TtError.Exception(e));
            }
        }

        public static TtResult<T2> Select<T, T2>(this TtResult<T> resultIn, Func<T2> func)
        {
            if (func is null) return TtResult<T2>.Fail(TtError.ArgumentNullError());
            if (resultIn.Failed) return TtResult<T2>.Fail(resultIn.Errors);
            return resultIn.Select(inp => func());
        }
    }
}