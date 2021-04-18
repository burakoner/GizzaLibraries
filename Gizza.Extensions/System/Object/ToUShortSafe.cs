using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static ushort ToUShortSafe(this object @this)
        {
            ushort result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) ushort.TryParse(strSafe, out result);
            return result;
        }
    }
}