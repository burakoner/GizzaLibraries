using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string ReplaceLastOccurrence(this string @this, string Find, string Replace)
        {
            int Place = @this.LastIndexOf(Find);
            return @this.Remove(Place, Find.Length).Insert(Place, Replace);
        }
    }
}