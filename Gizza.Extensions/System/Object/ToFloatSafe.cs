using System;
using System.Text;
namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static float ToFloatSafe(this object @this)
        {
            float result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) float.TryParse(strSafe, out result);
            return result;
        }
    }
}