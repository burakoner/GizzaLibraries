using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A DateTime extension method that query if '@this' is in the past.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if the value is in the past, false if not.</returns>
        public static bool IsPast(this DateTime @this, DateTimeCompareWith compareWith = DateTimeCompareWith.Now)
        {
            return @this < (compareWith == DateTimeCompareWith.Now ? DateTime.Now : compareWith == DateTimeCompareWith.UtcNow ? DateTime.UtcNow : DateTime.Now);
        }
    }
}