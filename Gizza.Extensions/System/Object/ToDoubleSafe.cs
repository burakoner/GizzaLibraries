using System;
using System.Text;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static double ToDoubleSafe(this object @this)
        {
            double result = 0;
            string strSafe = @this.ToStringSafe();
            if (strSafe.IsNotNullOrEmpty()) double.TryParse(strSafe, out result);
            return result;
        }
    }
}