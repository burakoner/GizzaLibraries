using System;
using System.Text.RegularExpressions;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static int CountWords(this string @this)
        {
            var count = 0;
            try
            {
                // Exclude whitespaces, Tabs and line breaks
                var re = new Regex(@"[^\s]+");
                var matches = re.Matches(@this);
                count = matches.Count;
            }
            catch
            {
            }
            return count;
        }
    }
}