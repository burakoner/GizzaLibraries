using System.Text.RegularExpressions;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' is numeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if numeric, false if not.</returns>
        public static bool IsNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^0-9]");
        }

        public static bool IsNumeric(this string @this, bool acceptDot = true)
        {
            foreach (char c in @this)
            {
                if (!char.IsDigit(c) && ((acceptDot && c != '.') || (!acceptDot)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}