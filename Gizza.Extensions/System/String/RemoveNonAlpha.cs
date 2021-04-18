using System;
using System.Linq;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string RemoveNonAlpha(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => Char.IsLetter(x)).ToArray());
        }
    }
}