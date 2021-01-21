using System;
using System.Globalization;
using System.Reflection;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A DateTime extension method that first day of week.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A DateTime.</returns>
        public static DateTime FirstDayOfWeek(this DateTime @this, DayOfWeek startDayOfWeek = DayOfWeek.Monday)
        {
            var diff = (int)@this.DayOfWeek - (int)startDayOfWeek;
            if (diff < 0) diff += 7;
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(-diff);
        }

        /*
        public static DateTime FirstDayOfWeek(this DateTime @this)
        {
            return new DateTime(@this.Year, @this.Month, @this.Day).AddDays(-(int)@this.DayOfWeek);
        }
        */
    }
}