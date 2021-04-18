using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static int CountChars(this string @this)
        {
            return @this.Length;
        }
    }
}