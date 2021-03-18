using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TwoTrackResult.Utilities
{
    internal class Meta
    {
        public static string GetMethodSignature(object currentObject, MethodBase currentMethod)
        {
            var methodSignature = currentMethod
                .ToString()
                .Split(' ')
                .Skip(1);
            return $"{currentObject}.{string.Join(" ", methodSignature)}";
        }
    }
}
