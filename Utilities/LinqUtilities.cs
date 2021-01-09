using System;
using System.Collections.Generic;
using System.Linq;

namespace SPZCapstoneVar2.Utilities
{
    public static class LinqUtilities
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) => source.ToList().ForEach(action);
    }
}
