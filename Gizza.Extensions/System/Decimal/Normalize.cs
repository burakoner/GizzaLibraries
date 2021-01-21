using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static decimal Normalize(this decimal value)
        {
            return value / 1.000000000000000000000000000000000m;
        }
    }
}