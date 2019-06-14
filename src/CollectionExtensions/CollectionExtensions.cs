using System;
using System.Collections.Generic;
using System.Linq;
using Enable.Common;

namespace Enable.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> collection)
        {
            Argument.IsNotNull(source, "source");
            Argument.IsNotNull(collection, "collection");

            foreach (var item in collection)
            {
                source.Add(item);
            }
        }

        public static void RemoveRange<T>(this ICollection<T> source, IEnumerable<T> collection)
        {
            Argument.IsNotNull(source, "source");
            Argument.IsNotNull(collection, "collection");

            foreach (var item in collection)
            {
                source.Remove(item);
            }
        }

        public static List<List<T>> Batch<T>(this ICollection<T> source, int batchSize)
        {
            Argument.IsNotNull(source, "source");
            Argument.IsInRange(batchSize > 0, "batchSize");

            var allBatches = new List<List<T>>();

            if (source.Count() > 0)
            {
                var totalNumberOfBatches = (int)Math.Ceiling((float)source.Count / (float)batchSize);

                for (var i = 0; i < totalNumberOfBatches; i++)
                {
                    var batch = source.Skip(i * batchSize).Take(batchSize).ToList();
                    allBatches.Add(batch);
                }
            }

            return allBatches;
        }

        public static IEnumerable<T> DistinctBy<T, TProp>(this IEnumerable<T> source, Func<T, TProp> property)
        {
            Argument.IsNotNull(source, "source");
            Argument.IsNotNull(property, "property");

            return source.GroupBy(property).Select(o => o.First());
        }
    }
}
