using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static uint ToUIntSafe(this object @this)
        {
            uint result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) uint.TryParse(strSafe, out result);
            return result;
        }
    }
}