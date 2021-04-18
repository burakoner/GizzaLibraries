using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A DateTime extension method that query if '@this' is now.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if now, false if not.</returns>
        public static bool IsNow(this DateTime @this, DateTimeCompareWith compareWith = DateTimeCompareWith.Now)
        {
            return @this == (compareWith == DateTimeCompareWith.Now ? DateTime.Now : compareWith == DateTimeCompareWith.UtcNow ? DateTime.UtcNow : DateTime.Now);
        }
    }
}