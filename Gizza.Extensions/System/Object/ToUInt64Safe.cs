using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static UInt64 ToUInt64Safe(this object @this)
        {
            UInt64 result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) UInt64.TryParse(strSafe, out result);
            return result;
        }
    }
}