using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static int CountLines(this string @this, int charsPerLine = 300)
        {
            return Convert.ToInt32(Math.Ceiling((double)@this.Length / (double)charsPerLine));
        }
    }
}