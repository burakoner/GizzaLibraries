using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static decimal ToDecimalSafe(this object @this)
        {
            decimal result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) decimal.TryParse(strSafe, out result);
            return result;
        }
    }
}