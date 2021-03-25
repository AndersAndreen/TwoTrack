﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult.ExtensionMethods
{
    public static class TtResultGenericValueOrDefault
    {
        public static T ValueOrDefault<T>(this ITwoTrack<T> result, T defaultValue)
        {
            if (result is null) throw new ArgumentNullException(nameof(result));
            var outputValue = defaultValue;
            result.Select(value => outputValue = value);
            return outputValue;
        }
    }
}
