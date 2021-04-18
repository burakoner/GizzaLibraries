using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// Get string value before [first] a.
        /// </summary>
        public static string Before(this string @this, string a)
        {
            int posA = @this.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return @this.Substring(0, posA);
        }
    }
}