using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static DateTime FromUnixTimeMilliSeconds(this int @this)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(@this / 1000);
        }
        public static DateTime FromUnixTimeMilliSeconds(this long @this)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(@this / 1000);
        }
    }
}