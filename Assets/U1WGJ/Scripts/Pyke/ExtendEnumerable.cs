using System;
using System.Collections.Generic;

namespace Pyke
{
    public static class ExtendEnumerable
    {
        public static IEnumerable<(T item, int index)> Index<T>(this IEnumerable<T> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            IEnumerable<(T item, int index)> impl()
            {
                var i = 0;
                foreach (var item in source)
                {
                    yield return (item, i);
                    ++i;
                }
            }

            return impl();
        }
    }
}
