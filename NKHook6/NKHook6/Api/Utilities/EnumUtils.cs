using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace NKHook6.Api.Utilities
{
    public class EnumUtils
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}
