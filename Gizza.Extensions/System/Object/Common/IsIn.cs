using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A T extension method to determines whether the object is equal to any of the provided values.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="this">The object to be compared.</param>
        /// <param name="values">The value list to compare with the object.</param>
        /// <returns>true if the values list contains the object, else false.</returns>
        public static bool IsIn<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
    }
}