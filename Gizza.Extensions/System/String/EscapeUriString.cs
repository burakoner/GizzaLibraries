using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string EscapeUriString(this string @this)
        {
            return Uri.EscapeUriString(@this);
        }
    }
}