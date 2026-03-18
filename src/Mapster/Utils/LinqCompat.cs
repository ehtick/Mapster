using System;
using System.Collections.Generic;
using System.Linq;

namespace Mapster.Utils
{
    internal static class LinqCompat
    {
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(
            IEnumerable<TSource> source,
            IEnumerable<TKey> keys,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

#if NET8_0_OR_GREATER
            return source.IntersectBy(keys, keySelector, comparer);
#else
            return IntersectByFallback(source, keys, keySelector, comparer);
#endif
        }

        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(
            IEnumerable<TSource> source,
            IEnumerable<TKey> keys,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (keys == null)
                throw new ArgumentNullException(nameof(keys));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

#if NET8_0_OR_GREATER
            return source.ExceptBy(keys, keySelector, comparer);
#else
            return ExceptByFallback(source, keys, keySelector, comparer);
#endif
        }

#if !NET8_0_OR_GREATER
        private static IEnumerable<TSource> IntersectByFallback<TSource, TKey>(
            IEnumerable<TSource> source,
            IEnumerable<TKey> keys,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            var keySet = new HashSet<TKey>(keys, comparer);
            foreach (var item in source)
            {
                if (keySet.Remove(keySelector(item)))
                    yield return item;
            }
        }

        private static IEnumerable<TSource> ExceptByFallback<TSource, TKey>(
            IEnumerable<TSource> source,
            IEnumerable<TKey> keys,
            Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? comparer)
        {
            var keySet = new HashSet<TKey>(keys, comparer);
            var seen = new HashSet<TKey>(comparer);
            foreach (var item in source)
            {
                var key = keySelector(item);
                if (keySet.Contains(key) || !seen.Add(key))
                    continue;
                yield return item;
            }
        }
#endif
    }
}
