using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the larger of two 16-bit signed integers.
        /// </summary>
        /// <param name="val1">The first of two 16-bit signed integers to compare.</param>
        /// <param name="val2">The second of two 16-bit signed integers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static UInt16 Max(this UInt16 val1, UInt16 val2)
        {
            return Math.Max(val1, val2);
        }
    }
}