using System;
using System.Linq;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string RemoveNonNumeric(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => Char.IsNumber(x)).ToArray());
        }
    }
}