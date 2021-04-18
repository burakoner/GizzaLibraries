using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="d"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public static DateTime AddWorkdays(this DateTime @this, int days)
        {
            // start from a weekday
            while (@this.IsWeekDay()) @this = @this.AddDays(1.0);
            for (int i = 0; i < days; ++i)
            {
                @this = @this.AddDays(1.0);
                while (@this.IsWeekDay()) @this = @this.AddDays(1.0);
            }
            return @this;
        }
    }
}