using System;
using System.Collections.Generic;
using System.Text;

namespace TwoTrackResult.Utilities
{
    internal static class ListUtilities
    {
        public static List<T> ValueToList<T>(this T value) => new List<T> {value};
    }
}
