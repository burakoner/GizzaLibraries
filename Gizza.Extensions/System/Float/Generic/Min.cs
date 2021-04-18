using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the smaller of two float.
        /// </summary>
        /// <param name="@this">The first of two float to compare.</param>
        /// <param name="val2">The second of two float to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static float Min(this float @this, float val2)
        {
            return Math.Min(@this, val2);
        }
    }
}