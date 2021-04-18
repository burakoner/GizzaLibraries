using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static ulong ToULongSafe(this object @this)
        {
            ulong result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) ulong.TryParse(strSafe, out result);
            return result;
        }
    }
}