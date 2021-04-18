using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {

        /// <summary>
        /// Finds the last day of the year for the current day (today).
        /// </summary>
        /// <returns>The last day of the current year.</returns>
        static public DateTime LastDayOfYear()
        {
            return LastDayOfYear(DateTime.Today);
        }

        /// <summary>
        /// Finds the last day of the year for the selected day's year.
        /// </summary>
        /// <param name="dateTime">The day that falls in the year.</param>
        /// <returns>The last day of the year.</returns>
        static public DateTime LastDayOfYear(DateTime dateTime)
        {
            // Find last day of the current year.
            return new DateTime(dateTime.Year, 12, 31);
        }
    }
}