using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishSelfTraining.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static Y Use<T, Y>(this T item, Func<T, Y> func) => func(item);
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.Any();
        public static bool IsNotEmpty<T>(this IEnumerable<T> enumerable) => !enumerable.IsEmpty();
        public static bool Opposite(this bool value) => !value;
        public static IReadOnlyCollection<T> AsReadOnlyList<T>(this IEnumerable<T> enumerable) => 
            (enumerable as List<T> ?? enumerable.ToList()).AsReadOnly();

    }
}
