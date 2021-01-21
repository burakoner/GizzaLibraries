using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static long ToLongSafe(this object @this)
        {
            var decValue = 0.0m;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) decimal.TryParse(strSafe, out decValue);
            return (long)decValue;
        }
    }
}