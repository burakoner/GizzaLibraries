using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static long ToUnixTimeSeconds(this DateTime @this)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((@this - epoch).TotalSeconds);
        }
    }
}