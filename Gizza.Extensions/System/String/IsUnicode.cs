using System.Text;
using System.Text.RegularExpressions;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static bool IsUnicode(this string @this)
        {
            int asciiBytesCount = Encoding.ASCII.GetByteCount(@this);
            int unicodBytesCount = Encoding.UTF8.GetByteCount(@this);

            return asciiBytesCount != unicodBytesCount;
        }
    }
}