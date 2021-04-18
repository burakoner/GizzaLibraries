using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static DateTime LastDayOfMonth(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static string LastDayOfMonth(this DateTime dateTime, string format = "dd/MM/yyyy")
        {
            return new DateTime(dateTime.Year, dateTime.Month + 1, 1).AddDays(-1).ToString(format);
        }
    }
}