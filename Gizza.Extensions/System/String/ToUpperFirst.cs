using System;
using System.Linq;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string ToUpperFirst(this string @this)
        {
            if (string.IsNullOrEmpty(@this))
            {
                return string.Empty;
            }

            char[] theChars = @this.ToCharArray();
            theChars[0] = char.ToUpper(theChars[0]);

            return new string(theChars);
        }
    }
}