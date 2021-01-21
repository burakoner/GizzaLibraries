using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static string FirstDayOfMonth(this DateTime dateTime, string format = "dd/MM/yyyy")
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).ToString(format);
        }
    }
}