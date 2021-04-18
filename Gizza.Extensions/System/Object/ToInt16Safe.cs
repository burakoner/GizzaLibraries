using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static Int16 ToInt16Safe(this object @this)
        {
            Int16 result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) Int16.TryParse(strSafe, out result);
            return result;
        }
    }
}