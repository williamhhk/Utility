using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class IEnumeratorExtension
    {
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerator source)
        {
            IEnumerable<T> collections = Enumerable.Empty<T>();
            while (source.MoveNext())
            {
                var t = (T)source.Current;
                collections = collections.Concat(new[] { t });
            }
            return collections;
        }
    }
}
