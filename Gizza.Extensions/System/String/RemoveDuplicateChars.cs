using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string RemoveDuplicateChars(this string @this, char Dupe)
        {
            return @this.RemoveDuplicateChars(new char[] { Dupe });
        }

        public static string RemoveDuplicateChars(this string @this, char[] Dupes)
        {
            // Örnek:
            // textBox2.Text = textBox1.Text.RemoveDuplicateChars(new char[] { ' ', '.', 'm' });
            var sd = (char[])Dupes.Clone();
            Array.Sort(sd);

            var res = new StringBuilder(@this.Length);

            for (int i = 0; i < @this.Length; i++)
            {
                if (i == 0 || @this[i] != @this[i - 1] || Array.BinarySearch(sd, @this[i]) < 0)
                {
                    res.Append(@this[i]);
                }
            }
            return res.ToStringSafe();
        }
    }
}