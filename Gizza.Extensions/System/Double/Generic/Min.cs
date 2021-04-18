using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the smaller of two decimals.
        /// </summary>
        /// <param name="@this">The first of two decimals to compare.</param>
        /// <param name="val2">The second of two decimals to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static double Min(this double @this, double val2)
        {
            return Math.Min(@this, val2);
        }
    }
}