using System.Text;
using System.Text.RegularExpressions;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        /// <summary>A string extension method that strip HTML.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string StripHtml(this string @this)
        {
            var path = new StringBuilder(@this);
            var sb = new StringBuilder();

            int pos = 0;

            while (pos < path.Length)
            {
                char ch = path[pos];
                pos++;

                if (ch == '<')
                {
                    // LOOP until we close the html tag
                    while (pos < path.Length)
                    {
                        ch = path[pos];
                        pos++;

                        if (ch == '>')
                        {
                            break;
                        }

                        if (ch == '\'')
                        {
                            // SKIP attribute starting with single quote
                            pos = GetIndexAfterNextSingleQuote(path, pos, true);
                        }
                        else if (ch == '"')
                        {
                            // SKIP attribute starting with double quote
                            pos = GetIndexAfterNextDoubleQuote(path, pos, true);
                        }
                    }
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        /*
        /// <summary>
        /// Used when we want to completely remove HTML code and not encode it with XML entities.
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static string StripHtml(this string Source)
        {
            if (string.IsNullOrEmpty(Source))
                return string.Empty;

            return Regex.Replace(Source, @"<[^>]*>", string.Empty);
        }
        */
    }
}