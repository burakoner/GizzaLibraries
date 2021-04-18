using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the larger of two float.
        /// </summary>
        /// <param name="@this">The first of two float to compare.</param>
        /// <param name="val2">The second of two float to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static float Max(this float @this, float val2)
        {
            return Math.Max(@this, val2);
        }
    }
}