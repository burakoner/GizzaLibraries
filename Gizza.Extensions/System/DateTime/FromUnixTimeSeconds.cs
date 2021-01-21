using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static DateTime FromUnixTimeSeconds(this int @this)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(@this);
        }

        public static DateTime FromUnixTimeSeconds(this long @this)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(@this);
        }
    }
}