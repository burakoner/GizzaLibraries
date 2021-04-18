using System;

namespace Gizza.Extensions
{
    public static partial class Extensions
    {
        public static string XmlEntities(this string @this)
        {
            string Output = @this;
            Output = Output.Replace("&", "&amp;");
            Output = Output.Replace("<", "&lt;");
            Output = Output.Replace(">", "&gt;");
            Output = Output.Replace("'", "&apos;");
            Output = Output.Replace("\"", "&quot;");
            return Output;
        }
    }
}