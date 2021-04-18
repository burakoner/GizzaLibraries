using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static long ToUInt32Safe(this object @this)
        {
            UInt32 result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) UInt32.TryParse(strSafe, out result);
            return result;
        }
    }
}