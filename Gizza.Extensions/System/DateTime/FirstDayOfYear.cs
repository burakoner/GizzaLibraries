using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {

        /// <summary>
        /// Overloaded method to return the first day of the current year
        /// (from today).
        /// </summary>
        /// <returns>The first day of the current year.</returns>
        static public DateTime FirstDayOfYear()
        {
            return FirstDayOfYear(DateTime.Today);
        }

        /// <summary>
        /// Finds the first day of year in which the selected day
        /// falls (as in the first day of that year).
        /// </summary>
        /// <param name="dateTime">The day of the year you want
        /// the first day of.</param>
        /// <returns>The first day of the year.</returns>
        static public DateTime FirstDayOfYear(DateTime dateTime)
        {
            // First day of the current year.
            return new DateTime(dateTime.Year, 1, 1);
        }
    }
}