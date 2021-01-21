using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static Int64 ToInt64Safe(this object @this)
        {
            var decValue = 0.0m;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) decimal.TryParse(strSafe, out decValue);
            return (Int64)decValue;
        }
    }
}