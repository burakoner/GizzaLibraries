using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the larger of two decimals.
        /// </summary>
        /// <param name="@this">The first of two decimals to compare.</param>
        /// <param name="val2">The second of two decimals to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static double Max(this double @this, double val2)
        {
            return Math.Max(@this, val2);
        }
    }
}