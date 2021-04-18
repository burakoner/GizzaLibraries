using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string BetweenFirstAndLast(this string @this, string a, string b)
        {
            int posA = @this.IndexOf(a);
            int posB = @this.LastIndexOf(b);
            if (posA == -1)
            {
                return "";
            }
            if (posB == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= posB)
            {
                return "";
            }
            return @this.Substring(adjustedPosA, posB - adjustedPosA);
        }
    }
}