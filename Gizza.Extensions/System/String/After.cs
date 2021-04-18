using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        public static string After(this string @this, string a)
        {
            int posA = @this.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= @this.Length)
            {
                return "";
            }
            return @this.Substring(adjustedPosA);
        }
    }
}