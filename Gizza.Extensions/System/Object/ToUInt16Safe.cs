using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static UInt16 ToUInt16Safe(this object @this)
        {
            UInt16 result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) UInt16.TryParse(strSafe, out result);
            return result;
        }
    }
}