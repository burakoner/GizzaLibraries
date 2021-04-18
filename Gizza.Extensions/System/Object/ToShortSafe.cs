using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static short ToShortSafe(this object @this)
        {
            short result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) short.TryParse(strSafe, out result);
            return result;
        }
    }
}