using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     Returns the absolute value of a decimal signed integer.
        /// </summary>
        /// <param name="@this">A number that is greater than , but less than or equal to .</param>
        /// <returns>A decimal signed integer, x, such that 0 ? x ?.</returns>
        public static decimal Abs(this decimal @this)
        {
            return Math.Abs(@this);
        }
    }
}